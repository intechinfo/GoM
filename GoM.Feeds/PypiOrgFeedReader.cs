using GoM.Core;
using GoM.Core.Mutable;
using GoM.Feeds.Abstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class PypiOrgFeedReader : PypiFeedReader
    {
        HttpClient _client;
        string _baseUrl = "http://registry.npmjs.org/";
        internal PypiOrgFeedReader()
        {
            _client = new HttpClient();
        }

        public async override Task<bool> FeedMatch(Uri adress)
        {
            HttpResponseMessage response = await _client.GetAsync(adress);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ArgumentException("Package could not be found");
                }
                throw new InvalidOperationException("an error occured while request server status code:" + response.StatusCode);
            }
            string resp = await response.Content.ReadAsStringAsync();

            JObject o;
            try
            {
                o = JObject.Parse(resp);
            }
            catch (JsonReaderException)
            {
                return false;
            }
            if (!o.HasValues) throw new InvalidOperationException("No data found from " + adress.ToString() + " .");
            bool isPypi = o.TryGetValue("info", out JToken value);
            if (isPypi)
            {
                var datas = new JObject(o.Property("info"));
                return datas["name"].Value<string>() == "Python";
            }
            return false;
        }

        public override async Task<IEnumerable<IPackageInstance>> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            name = name.ToLowerInvariant();

            HttpResponseMessage response = await _client.GetAsync(_baseUrl + name + "/json");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ArgumentException("Package could not be found");
                }
                throw new InvalidOperationException("an error occured while request server status code:" + response.StatusCode);
            }
            string resp = await response.Content.ReadAsStringAsync();

            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " found.");

            var list = new List<IPackageInstance>();
            var versions = new JObject( new JObject(o["info"]).Property("releases"));
            foreach (var item in versions)
            {

                if (SemVersion.TryParse(item.Key, out SemVersion item_v))
                {
                    string packageVersion = item.Key;
                    string packageName = o["info"].Value<string>("name");
                    list.Add(new PackageInstance { Name = packageName, Version = packageVersion });
                }
            }
            return list;
        }

        public override async Task<IEnumerable<ITarget>> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            string resp = await _client.GetStringAsync(_baseUrl + name + '/' + version+"/json");
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " with version : " + version + " found.");
            o = new JObject(o.Property("info"));
            var list = new List<Target>();

            Regex reg = new Regex(@"^Programming Language :: Python :: \d([.]\d([.]\d)?)?$");
            foreach (var item in o.Property("classifiers"))
            {
                var val = item.Value<string>();
                if(reg.IsMatch(val)) list.Add(new Target { Name =val  });
            }
            bool hasDependencies = o.TryGetValue("requires_dist", out JToken t);
            if (hasDependencies)
            {
                JObject dependencies = new JObject(t);
               
                //iterate on eah version of the json
                foreach (var item in dependencies)
                {
                    string depName = item.Key;
                    string depVersion = item.Value.ToString();
                    list.ForEach((x) => x.Dependencies.Add(new TargetDependency { Name = depName, Version = depVersion }));
                }
            }
            return list;
        }

        public override async Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name, string version)
        {
            var res = await GetAllVersions(name);
            return res.Where(x => SemVersion.Parse(x.Version) > SemVersion.Parse(version));
        }
    }
}
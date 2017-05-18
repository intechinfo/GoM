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
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class NpmJsFeedReader : NpmFeedReader
    {
        HttpClient _client;
        string _baseUrl = "http://registry.npmjs.org/";
        public NpmJsFeedReader()
        {
            _client = new HttpClient();
        }

        public override void Dispose()
        {
            _client.Dispose();
        }

        public override async Task<bool> FeedMatch(Uri adress)
        {
            if (String.IsNullOrWhiteSpace(adress.OriginalString)) throw new ArgumentNullException("The Uril adress cannot be null or Empty");

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
            catch(JsonReaderException)
            {
                return false;
            }
            if (!o.HasValues) throw new InvalidOperationException("No data found from " + adress.ToString() +" .");
            bool isNpm = o.TryGetValue("db_name", out JToken value);
            if (isNpm)
            {
                return o.Property("db_name").Value.ToString() == "registry";
            }
            return false;
        }

        public override async Task<IEnumerable<IPackageInstance>> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            name = name.ToLowerInvariant();

            HttpResponseMessage response = await _client.GetAsync(_baseUrl + name);

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
            JObject versions = new JObject( o.Property("versions"));
            //iterate on eah version of the json
            foreach (var item in versions)
            {
                if (SemVersion.TryParse(item.Key, out SemVersion item_v))
                {
                    string packageVersion = item.Key;
                    string packageName = item.Value["name"].ToString();
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

            string resp = await _client.GetStringAsync(_baseUrl + name+'/'+version);
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " with version : "+version+" found.");

            JObject dependencies = new JObject( o.Property("dependencies"));
            var list = new List<ITarget>();
            var target = new Target { Name = o.Value<string>("name") };
            //iterate on eah version of the json
            foreach (var item in dependencies)
            {
                string depName = item.Key;
                string depVersion = item.Value.ToString();
                target.Dependencies.Add(new TargetDependency { Name = depName, Version = depVersion });                
            }
            list.Add(target);
            return list;
        }

        public override async Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name, string version)
        {
            var res = await GetAllVersions(name);
            return res.Where(x => SemVersion.Parse(x.Version) > SemVersion.Parse(version));
        }
    }
}

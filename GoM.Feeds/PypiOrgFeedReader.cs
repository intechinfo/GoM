using GoM.Core;
using GoM.Core.Mutable;
using GoM.Feeds.Abstractions;
using GoM.Feeds.Results;
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
    public class PypiOrgFeedReader : FeedReaderBase
    {
        string _baseUrl = "https://pypi.python.org/pypi/Python/json";

        public override async Task<FeedMatchResult> FeedMatch(Uri adress)
        {
            if (String.IsNullOrWhiteSpace(adress.OriginalString)) throw new ArgumentNullException("The Uril adress cannot be null or Empty");

            var result = await GetJson(adress);
            if (result.Success)
            {
                JObject o = result.Result;

                if (!o.HasValues) throw new InvalidOperationException("No data found from " + adress.ToString() + " .");
                bool isPypi = o.TryGetValue("info", out JToken value);
                if (isPypi)
                {
                    return new FeedMatchResult(null,o.Property("info").Value.ToString() == "registry");
                }
                return new FeedMatchResult(null, false);
            }
            return new FeedMatchResult(result.NetworkException ?? result.JsonException, false);
        }


        public override async Task<GetPackagesResult> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            name = name.ToLowerInvariant();

            var result = await GetJson(new Uri( _baseUrl + name));
            if (result.Success)
            {
                JObject o = result.Result;
                if (!o.HasValues )
                {
                    return new GetPackagesResult(new InvalidOperationException("No package named : " + name + " found."),null);
                }
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
                return new GetPackagesResult(null, list);
                
            }

        }

        public override async Task<GetDependenciesResult> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            string resp = await HttpClient.GetStringAsync(_baseUrl + name + '/' + version+"/json");
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
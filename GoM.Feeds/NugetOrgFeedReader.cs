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
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class NugetOrgFeedReader : FeedReaderBase
    {
        string _baseUrl = "https://api.nuget.org/v3/registration1/";

        public override async Task<FeedMatchResult> FeedMatch(Uri adress)
        {
            if (String.IsNullOrWhiteSpace(adress.OriginalString))
                throw new ArgumentNullException("adress must be not null");

            var json = await GetJson(adress);

            if (json.Success)
            {
                JObject o = json.Result;
                if (!o.HasValues) throw new InvalidOperationException("No data found from " + adress.ToString() + " .");

                if (o.TryGetValue("version", out JToken j1) && o.TryGetValue("resources", out JToken j2) && o.TryGetValue("@context", out JToken j3))
                    return new FeedMatchResult(null, true);

                return new FeedMatchResult(null, false);
            }
            else
            {
                return new FeedMatchResult(json.NetworkException == null ? json.JsonException : json.NetworkException, false);
            }           
        }

        public override async Task<IEnumerable<PackageInstanceResult>> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            //name = name.ToLowerInvariant();

            var json = await GetJson(new Uri("http://api.nuget.org/v3-flatcontainer/" + name + "/index.json"));

            if (json.Success)
            {
                JObject o = json.Result;
                if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " found.");

                if (o.TryGetValue("version", out JToken j1) && o.TryGetValue("resources", out JToken j2) && o.TryGetValue("@context", out JToken j3))
                    return new FeedMatchResult(null, true);

                return new FeedMatchResult(null, false);
            }
            else
            {
                return new FeedMatchResult(json.NetworkException == null ? json.JsonException : json.NetworkException, false);
            }


            HttpResponseMessage response = await HttpClient.GetAsync("http://api.nuget.org/v3-flatcontainer/" + name + "/index.json");

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new ArgumentException("Package could not be found" );
                }
                throw new InvalidOperationException("an error occured while request server status code:" + response.StatusCode);
            }
            string resp = await response.Content.ReadAsStringAsync();

            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " found.");

            var list = new List<IPackageInstance>();
            JArray versions = o["versions"] as JArray;
            //iterate on eah version of the json
            foreach (var item in versions)
            {
                if (SemVersion.TryParse(item.ToString(), out SemVersion item_v))
                {
                    string packageVersion = item.ToString();
                    list.Add(new PackageInstance { Name = name, Version = packageVersion });
                }
            }
            return list;
        }
        public override async Task<IEnumerable<TargetResult>> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            string resp = await HttpClient.GetStringAsync(_baseUrl + name + '/' + version + ".json");
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " with version : " + version + " found.");

            string respDependencies = await HttpClient.GetStringAsync(o.Value<string>("catalogEntry"));

            o = JObject.Parse(respDependencies);

            var dependencies = o.Property("dependencyGroups").HasValues ? o.Property("dependencyGroups").Value<JObject>().Property("dependencies").Value<JArray>() : null;

            var list = new List<ITarget>();

            if (dependencies.HasValues)
            {
                var target = new Target { Name = o.Value<string>("title") };

                //iterate on eah version of the json
                foreach (var item in dependencies)
                {
                    var currentObj = new JObject(item);

                    string depName = currentObj.Property("id").ToString();
                    string depVersion = currentObj.Property("range").ToString();

                    if (depVersion[0] == '[')
                        depVersion = depVersion.Substring(1, depVersion.Length - 3);

                    target.Dependencies.Add(new TargetDependency { Name = depName, Version = depVersion });
                }
                list.Add(target);
            }
            return list;
        }

        public override async Task<IEnumerable<PackageInstanceResult>> GetNewestVersions(string name, string version)
        {
            var res = await GetAllVersions(name);
            var ver = SemVersion.Parse(version);
            return res.Where(x => SemVersion.Parse(x.Version) > ver);
        }
    }
}

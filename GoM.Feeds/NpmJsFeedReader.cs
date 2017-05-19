using GoM.Core.Mutable;
using GoM.Feeds.Results;
using Newtonsoft.Json.Linq;
using Semver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class NpmJsFeedReader : FeedReaderBase
    {
        string _baseUrl = "http://registry.npmjs.org/";

        public override async Task<FeedMatchResult> FeedMatch(Uri adress)
        {
            if (String.IsNullOrWhiteSpace(adress.OriginalString)) throw new ArgumentNullException("The Uril adress cannot be null or Empty");


            var result = await GetJson(adress);
            if (result.Success)
            {
                JObject o = result.Result;

                if (!o.HasValues)
                {
                    return new FeedMatchResult(new InvalidOperationException("No data found at "+adress+" ."), false,result,this);
                }
                bool isNpm = o.TryGetValue("db_name", out JToken value);
                if (isNpm)
                {
                    return new FeedMatchResult(null,o.Property("db_name").Value.ToString() == "registry", result,this);
                }
                return new FeedMatchResult(null, false,result,this);
            }
            return new FeedMatchResult(result.NetworkException ?? result.JsonException, false, result,this);
        }

        public override async Task<ReadPackagesResult> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            name = name.ToLowerInvariant();

            var result = await GetJson(new Uri( _baseUrl + name));
            if (result.Success)
            {
                JObject o = result.Result;
                if (!o.HasValues)
                {
                   return new ReadPackagesResult(new InvalidOperationException("No package named : " + name + " found."),null, result);
                }

                var list = new List<PackageInstanceResult>();
                JObject versions = new JObject(result.Result.Property("versions"));
                //iterate on eah version of the json
                foreach (var item in versions)
                {
                    if (SemVersion.TryParse(item.Key, out SemVersion item_v))
                    {
                        string packageVersion = item.Key;
                        string packageName = item.Value["name"].ToString();
                        list.Add(new PackageInstanceResult(null, new PackageInstance { Name = packageName, Version = packageVersion }));
                    }
                    else
                    {
                        list.Add(new PackageInstanceResult(new ArgumentException("the version : "+item.Key+"is not Server Compliant"), null));
                    }
                }
                return new ReadPackagesResult(null,list, result);
            }
            else
            {
                return new ReadPackagesResult(result.NetworkException ?? result.JsonException, null, result);
            }
        }
        public override async Task<ReadDependenciesResult> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            var result = await GetJson(new Uri(_baseUrl + name + '/' + version));
            if (result.Success)
            {
                JObject o = result.Result;
                if (!o.HasValues)
                {
                    return new ReadDependenciesResult(new InvalidOperationException("No package named : " + name + " found."), null);
                }
                JObject dependencies = new JObject(o.Property("dependencies"));
                var list = new List<TargetResult>();
                var target = new Target { Name = o.Value<string>("name") };
                //iterate on eah version of the json
                foreach (var item in dependencies)
                {
                    string depName = item.Key;
                    string depVersion = item.Value.ToString();
                    target.Dependencies.Add(new TargetDependency { Name = depName, Version = depVersion });
                }
                list.Add(new TargetResult(null, target));
                return new ReadDependenciesResult(null, list);
            }
            else
            {
                return new ReadDependenciesResult(result.NetworkException ?? result.JsonException, null);
            }
        }

        public override async Task<ReadPackagesResult> GetNewestVersions(string name, string version)
        {
            if (!SemVersion.TryParse(version, out SemVersion refSemver))
                throw new ArgumentException("the version: " + version + " is not Server Compliant ");

            var res = await GetAllVersions(name);
            if (res.Success) return new ReadPackagesResult(null, res.Result.Where(x => !x.Success || SemVersion.Parse(x.Result.Version) > refSemver), res.Json);
            else return res;
        }
    }
}

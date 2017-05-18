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

                if (!o.HasValues)
                {
                    return new FeedMatchResult(new InvalidOperationException("No data found at " + adress + " ."), false, result, this);
                }
                bool isPypi = o.TryGetValue("info", out JToken value);
                if (isPypi)
                {
                    return new FeedMatchResult(null,o.Property("info").Value.ToString() == "registry",result, this);
                }
                return new FeedMatchResult(null, false,result, this);
            }
            return new FeedMatchResult(result.NetworkException ?? result.JsonException, false,result, this);
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
                var list = new List<PackageInstanceResult>();
                var versions = new JObject( new JObject(o["info"]).Property("releases"));
                foreach (var item in versions)
                {

                    if (SemVersion.TryParse(item.Key, out SemVersion item_v))
                    {
                        string packageVersion = item.Key;
                        string packageName = o["info"].Value<string>("name");
                        var p = new PackageInstance { Name = packageName, Version = packageVersion };
                        list.Add(new PackageInstanceResult(null,p));
                    }
                }
                return new GetPackagesResult(null, list);
            }
            else
            {
                return new GetPackagesResult(result.NetworkException ?? result.JsonException, null);
            }
        }

        public override async Task<GetDependenciesResult> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            var result = await GetJson(new Uri(_baseUrl + name + '/' + version + "/json"));
            if (result.Success)
            {
                JObject o = result.Result;
                if (!o.HasValues)
                {
                    return new GetDependenciesResult(new InvalidOperationException("No package named : " + name + " found."), null);
                }
                o = new JObject(o.Property("info"));
                var list = new List<TargetResult>();

                Regex reg = new Regex(@"^Programming Language :: Python :: \d([.]\d([.]\d)?)?$");
                foreach (var item in o.Property("classifiers"))
                {
                    var val = item.Value<string>();
                    if (reg.IsMatch(val))
                    {
                        var tar = new Target { Name = val };
                        list.Add(new TargetResult(null,tar));
                    }
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
                        list.Where(x=>x.Success).ToList().ForEach(x => ((Target)(x.Result)).Dependencies.Add(new TargetDependency { Name = depName, Version = depVersion }));
                    }
                }
                return new GetDependenciesResult(null,list);
            }
            else
            {
                return new GetDependenciesResult(result.NetworkException ?? result.JsonException, null);
            }
        }

        public override async Task<GetPackagesResult> GetNewestVersions(string name, string version)
        {
            var res = await GetAllVersions(name);
            if (res.Success)
            {
                var packages = res.Result.Where(x => SemVersion.Parse(x.Result.Version) > SemVersion.Parse(version));
                return new GetPackagesResult(null, packages);
            }
            return res;
        }
    }
}
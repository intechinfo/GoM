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
                    return new FeedMatchResult(null, true,json,this);

                return new FeedMatchResult(null, false, json, this);
            }
            else
            {
                return new FeedMatchResult(json.NetworkException ?? json.JsonException , false, json,this);
            }
        }

        public override async Task<ReadPackagesResult> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");

            var json = await GetJson(new Uri("http://api.nuget.org/v3-flatcontainer/" + name + "/index.json"));

            if (json.Success)
            {
                JObject o = json.Result;
                if (!o.HasValues)
                    return new ReadPackagesResult(new InvalidOperationException("No package named : " + name + " found."), null);

                var list = new List<PackageInstanceResult>();
                JArray versions = o["versions"] as JArray;

                foreach (var item in versions)
                {
                    if (SemVersion.TryParse(item.ToString(), out SemVersion item_v))
                    {
                        string packageVersion = item.ToString();
                        var currentPackage = new PackageInstance { Name = name, Version = packageVersion };
                        list.Add(new PackageInstanceResult(null, new PackageInstance { Name = packageVersion, Version = packageVersion }));
                    }
                    else
                    {
                        list.Add(new PackageInstanceResult(new ArgumentException("the version : " + item + "is not Server Compliant"), null));
                    }
                }
                return new ReadPackagesResult(null, list);
            }
            else
            {
                return new ReadPackagesResult(json.NetworkException ?? json.JsonException, null);
            }
        }

        public override async Task<ReadDependenciesResult> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            var tryFoundPackage = await GetJson(new Uri(_baseUrl + name + '/' + version + ".json"));

            if (tryFoundPackage.Success)
            {
                if(!tryFoundPackage.Result.HasValues)
                    return new ReadDependenciesResult(new InvalidOperationException("No package named : " + name + " found."), null);

                JObject packageOverview = tryFoundPackage.Result;

                var tryGetPackageDetails = await GetJson(new Uri(packageOverview.Value<string>("catalogEntry")));

                if (tryGetPackageDetails.Success)
                {
                    if (!tryGetPackageDetails.Result.HasValues)
                        return new ReadDependenciesResult(new InvalidOperationException("Could not find details for package : " + name + "."), null);

                    try
                    {
                        var returnedDependecies = new List<TargetResult>();

                        var deps = tryGetPackageDetails.Result.Children<JProperty>().FirstOrDefault(x => x.Name == "dependencyGroups")?.Values<JObject>();
                        if (deps != null)
                        {
                            foreach(var targetFramework in deps)
                            {
                                var target = new Target { Name = targetFramework.Value<string>("targetFramework") };

                                var dependencys = targetFramework.Value<JArray>("dependencies");

                                foreach (var dependency in dependencys)
                                {
                                    string depName = dependency.Value<string>("id");
                                    string depVersion = dependency.Value<string>("range");

                                    if (depVersion[0] == '[')
                                        depVersion = depVersion.Substring(1, depVersion.Length - 2);

                                    target.Dependencies.Add(new TargetDependency { Name = depName, Version = depVersion });
                                }
                                returnedDependecies.Add(new TargetResult(null, target));
                            }
                        }
                        return new ReadDependenciesResult(null, returnedDependecies);
                    }
                    catch (Exception ex)
                    {
                        return new ReadDependenciesResult(ex, null);
                    }
                }
                else
                {
                    return new ReadDependenciesResult(tryGetPackageDetails.JsonException ?? tryGetPackageDetails.NetworkException, null);
                }
            }
            else
            {
                return new ReadDependenciesResult(tryFoundPackage.JsonException ?? tryFoundPackage.NetworkException, null);
            }
        }

        public override async Task<ReadPackagesResult> GetNewestVersions(string name, string version)
        {
            var res = await GetAllVersions(name);
            if (res.Success) return new ReadPackagesResult(null, res.Result.Where(x => !x.Success || SemVersion.Parse(x.Result.Version) > SemVersion.Parse(version)));
            else return res;
        }
    }
}

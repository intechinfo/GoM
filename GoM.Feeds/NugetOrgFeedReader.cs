using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GoM.Core.Mutable;

namespace GoM.Feeds
{
    internal class NugetOrgFeedReader : NugetFeedReader
    {
        HttpClient _client;
        string _baseUrl = "https://api.nuget.org/v3/registration1/";
        internal NugetOrgFeedReader()
        {
            _client = new HttpClient();
        }

        public override string BaseUrl
        {
            get { return _baseUrl; }
        }

        public override async Task<IEnumerable<IPackageInstance>> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            //name = name.ToLowerInvariant();
            string resp = await _client.GetStringAsync("http://api.nuget.org/v3-flatcontainer/" + name + "/index.json");
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " found.");

            var list = new List<IPackageInstance>();
            JObject versions = new JObject(o.Property("versions"));
            //iterate on eah version of the json
            foreach (var item in versions)
            {
                string packageVersion = item.Key;
                list.Add(new PackageInstance { Name = name, Version = packageVersion });
            }
            return list;
        }
        public override async Task<IEnumerable<ITarget>> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
            name = name.ToLowerInvariant();
            version = version.ToLowerInvariant();

            string resp = await _client.GetStringAsync(_baseUrl + name + '/' + version + ".json");
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " with version : " + version + " found.");

            string respDependencies = await _client.GetStringAsync(o.Value<string>("catalogEntry"));

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
    }
}

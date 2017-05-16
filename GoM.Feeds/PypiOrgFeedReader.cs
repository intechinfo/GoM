using GoM.Core;
using GoM.Core.Mutable;
using GoM.Feeds.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq;
namespace GoM.Feeds 
{
    internal class PypiOrgFeedReader : PypiFeedReader
    {
        HttpClient _client;
        string _baseUrl = "http://registry.npmjs.org/";
        internal PypiOrgFeedReader()
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
            name = name.ToLowerInvariant();
            string resp = await _client.GetStringAsync(_baseUrl + name+ "/json");
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " found.");

            var list = new List<IPackageInstance>();
            var versions = new JObject( new JObject(o["info"]).Property("releases"));
            foreach (var item in versions)
            {
                string packageName = o["info"].Value<string>("name");
                string packageVersion = item.Key ;

                list.Add(new PackageInstance { Name = packageName, Version = packageVersion });
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
    }
}
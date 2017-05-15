using GoM.Core;
using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GoM.Core.Mutable;
using System.Linq;
namespace GoM.Feeds
{
    internal class NpmJsFeedReader : NpmFeedReader
    {
        HttpClient _client;
        string _versionsListUrl = "http://registry.npmjs.org/";
        internal NpmJsFeedReader()
        {
            _client = new HttpClient();
        }

        public override async Task<IEnumerable<IPackageInstance>> GetAllVersions(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            string resp = await  _client.GetStringAsync(_versionsListUrl + name );
            JObject o = JObject.Parse(resp);
            if (!o.HasValues) throw new InvalidOperationException("No package named : " + name + " found.");

            var list = new List<IPackageInstance>();
            JObject versions = new JObject( o.Property("versions"));
            //iterate on eah version of the json
            foreach (var item in versions)
            {
                string packageVersion = item.Key;
                string packageName = item.Value["name"].ToString();
                list.Add(new PackageInstance { Name = packageName, Version = packageVersion });
            }
            return list;
        }
        public override async Task<IEnumerable<ITarget>> GetDependencies(string name, string version)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The parameter name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(version)) throw new ArgumentException("The parameter version cannot be null or empty.");
           
            string resp = await _client.GetStringAsync(_versionsListUrl + name+'/'+version);

            throw new NotImplementedException();
        }
    }
}

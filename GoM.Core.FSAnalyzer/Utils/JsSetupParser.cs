using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class JsSetupParser : BaseConfigParser
    {
        public JsSetupParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<Target> Read()
        {
            string jsonFileString = File.ReadAllText(this.Source.PhysicalPath + @"\package.json");
            JObject jObj = JObject.Parse(jsonFileString);
            List<TargetDependency> targetDependencies = new List<TargetDependency>();

            JProperty dependenciesJson = jObj.Property("dependencies");
            IJEnumerable<JToken> dependenciesTokens = dependenciesJson.Children().Children();
            int count = dependenciesTokens.Count();

            foreach (JToken item in dependenciesTokens)
            {
                var t = item.Values<JToken>();
                TargetDependency tDependendy = new TargetDependency
                {
                    Name = "test",
                    Version = dependenciesJson.Value.ToString()
                };
                targetDependencies.Add(tDependendy);
            }

            JProperty devDependenciesJson = jObj.Property("devDependencies");
            JEnumerable<JToken> devDependenciesTokens = devDependenciesJson.Children();

            foreach (JToken item in devDependenciesTokens)
            {
                TargetDependency tDependendy = new TargetDependency
                {
                    Name = devDependenciesJson.Name,
                    Version = devDependenciesJson.Value.ToString()
                };
                targetDependencies.Add(tDependendy);
            }

            return null;
        }
    }
}

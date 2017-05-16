using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class JsSetupParser : BaseConfigParser
    {
        public JsSetupParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<ITarget> Read()
        {
            string jsonFileString = File.ReadAllText(this.Source.PhysicalPath);
            JObject jObj = JObject.Parse(jsonFileString);
            List<TargetDependency> targetDependencies = new List<TargetDependency>();

            JProperty dependenciesJson = jObj.Property("dependencies");
            JEnumerable<JToken> dependenciesTokens = dependenciesJson.Children();

            foreach (JToken item in dependenciesTokens)
            {
                TargetDependency tDependendy = new TargetDependency
                {
                    Name = dependenciesJson.Name,
                    Version = (string) dependenciesJson.Value
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
                    Version = (string) devDependenciesJson.Value
                };
                targetDependencies.Add(tDependendy);
            }

            return null;
        }
    }
}

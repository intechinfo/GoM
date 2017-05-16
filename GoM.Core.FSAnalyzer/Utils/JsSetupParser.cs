using GoM.Core.Mutable;
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
        public JsSetupParser(string path) : base(path)
        {
        }

        public override IEnumerable<ITarget> Read()
        {
            string jsonFileString = File.ReadAllText(this.Path);
            JObject jObj = JObject.Parse(jsonFileString);
            List<TargetDependency> targetDependencies = new List<TargetDependency>();

            JProperty dependenciesJSON = jObj.Property("dependencies");
            JEnumerable<JToken> dependenciesTokens = dependenciesJSON.Children();

            foreach (JToken item in dependenciesTokens)
            {
                TargetDependency tDependendy = new TargetDependency();
                tDependendy.Name = dependenciesJSON.Name;
                tDependendy.Version = (string)dependenciesJSON.Value;
                targetDependencies.Add(tDependendy);
            }

            JProperty devDependenciesJSON = jObj.Property("devDependencies");
            JEnumerable<JToken> devDependenciesTokens = devDependenciesJSON.Children();

            foreach (JToken item in devDependenciesTokens)
            {
                TargetDependency tDependendy = new TargetDependency();
                tDependendy.Name = devDependenciesJSON.Name;
                tDependendy.Version = (string)devDependenciesJSON.Value;
                targetDependencies.Add(tDependendy);
            }

            return null;
        }
    }
}
}

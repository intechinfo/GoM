using GoM.Core.FSAnalyzer.Utils.JS;
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
            List<TargetDependency> targetDependencies = new List<TargetDependency>();

            string jsonFileString = File.ReadAllText(this.Source.PhysicalPath + @"\package.json");

            JObject jsConfigFileContent = JObject.Parse(jsonFileString);
            JObject dependencies = new JObject(jsConfigFileContent.Property("dependencies"));
            JObject devDependencies = new JObject(jsConfigFileContent.Property("devDependencies"));

            foreach (KeyValuePair<string, JToken> d in dependencies)
            {
                targetDependencies.Add(
                    new TargetDependency
                    {
                        Name = d.Key,
                        Version = d.Value.ToString()
                    }
                );
            }
            foreach (KeyValuePair<string, JToken> d in devDependencies)
            {
                targetDependencies.Add(
                    new TargetDependency
                    {
                        Name = d.Key,
                        Version = d.Value.ToString()
                    }
                );
            }

            Target target = new Target();
            target.Dependencies.AddRange(targetDependencies);

            List<Target> targets = new List<Target>();
            targets.Add(target);

            return targets;
        }
    }
}

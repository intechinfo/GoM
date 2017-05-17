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
    public class JsPackageParser : BaseConfigParser
    {
        public JsPackageParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<Target> Read()
        {
            List<TargetDependency> targetDependencies = new List<TargetDependency>();

            // Converts a file a a string
            string jsonFileString = File.ReadAllText(this.Source.PhysicalPath + @"\package.json");

            // Converts the string (JSon) to a JObject
            dynamic jsConfigFileContent = JObject.Parse(jsonFileString);

            // Checking if there are dependencies
            JObject dependencies = jsConfigFileContent["dependencies"] != null ? jsConfigFileContent["dependencies"] : null;

            if (dependencies != null)
            {
                targetDependencies.AddRange(CreateDependencies(dependencies));
            }

            // Checking if there are devDependencies and
            JObject devDependencies = jsConfigFileContent["devDependencies"] != null ? jsConfigFileContent["devDependencies"] : null;

            if (devDependencies != null)
            {
                targetDependencies.AddRange(CreateDependencies(devDependencies));
            }

            Target target = new Target();

            if (targetDependencies.Any())
                target.Dependencies.AddRange(targetDependencies);

            List<Target> targets = new List<Target> { target };

            return targets;
        }

        List<TargetDependency> CreateDependencies(JObject dependencies)
        {
            List<TargetDependency> targetDependenciesList = new List<TargetDependency>();

            if (dependencies.Count <= 0) return targetDependenciesList;
            foreach (KeyValuePair<string, JToken> d in dependencies)
            {
                targetDependenciesList.Add(new TargetDependency
                {
                    Name = d.Key,
                    Version = d.Value.ToString()
                });
            }
            return targetDependenciesList;
        }
    }
}

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

            // Converts a file a a string
            string jsonFileString = File.ReadAllText(this.Source.PhysicalPath + @"\package.json");

            // Converts the string (JSon) to a JObject
            dynamic jsConfigFileContent = JObject.Parse(jsonFileString);

            // Checking if there are dependencies
            dynamic dependencies = jsConfigFileContent["dependencies"] != null ? jsConfigFileContent["dependencies"] : null;

            if(dependencies != null)
            {
                targetDependencies.AddRange(CreateDependencies(dependencies));
            }

            // Checking if there are devDependencies and
            dynamic devDependencies = jsConfigFileContent["devDependencies"] != null ? jsConfigFileContent["devDependencies"] : null;

            if(devDependencies != null)
            {
                targetDependencies.AddRange(CreateDependencies(devDependencies));
            }

            Target target = new Target();

            if(targetDependencies.Count() > 0)
            {
                target.Dependencies.AddRange(targetDependencies);
            } else
            {
                List<TargetDependency> emptyTargetDependency = new List<TargetDependency>();
                target.Dependencies.AddRange(emptyTargetDependency);
            }

            List<Target> targets = new List<Target>();
            targets.Add(target);

            return targets;
        }

        List<TargetDependency> CreateDependencies(JObject dependencies)
        {
            List<TargetDependency> targetDependenciesList = new List<TargetDependency>();

            foreach (var d in dependencies)
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

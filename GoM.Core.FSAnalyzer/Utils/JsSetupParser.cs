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

            dynamic jsConfigFileContent = JsonConvert.DeserializeObject(jsonFileString);
            dynamic dependencies = jsConfigFileContent["dependencies"];
            dynamic devDependencies = jsConfigFileContent["devDependencies"];

            foreach (var d in dependencies)
            {
                targetDependencies.Add(
                    new TargetDependency
                    {
                        Name = d.Name,
                        Version = d.Value
                    }
                );
            }
            foreach (var d in devDependencies)
            {
                targetDependencies.Add(
                    new TargetDependency
                    {
                        Name = d.Name,
                        Version = d.Value
                    }
                );
            }
            Target target = new Target();
            target.Dependencies.AddRange(targetDependencies);
            IEnumerable<ITarget> targs = new List<ITarget>();
            targs.ToList().Add(target);
            return targs;
        }
    }
}

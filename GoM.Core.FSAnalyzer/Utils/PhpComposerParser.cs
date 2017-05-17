using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GoM.Core.Mutable;
using Newtonsoft.Json;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class PhpComposerParser: BaseConfigParser
    {
        IFileInfo _file;
        public PhpComposerParser(IFileInfo file) 
            : base( file )
        {
            _file = file;
        }

        public override IEnumerable<Target> Read()
        {
            List<TargetDependency> targets = new List<TargetDependency>();
            string fileContent = ReadFileContent();
            dynamic phpConfigFileContent = JsonConvert.DeserializeObject(fileContent);
            dynamic dependencies = phpConfigFileContent["require"];

            foreach( var d in dependencies )
            {
                targets.Add(
                    new TargetDependency
                    {
                        Name = d.Name,
                        Version = d.Value
                    }
                );
            }
            Target t = new Target();
            t.Dependencies.AddRange(targets);
            yield return t;
        }
    }
}

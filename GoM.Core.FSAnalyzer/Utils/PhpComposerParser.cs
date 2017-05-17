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

        public override IEnumerable<ITarget> Read()
        {
            List<TargetDependency> targets = new List<TargetDependency>();
            //PhysicalFileProvider pfp = new PhysicalFileProvider( _file );
            //Stream phpConfigFile = pfp.GetFileInfo( "./samplePhpComposer.json" ).CreateReadStream();
            //StreamReader sr = new StreamReader( phpConfigFile );
            string fileContent = sr.ReadToEnd();
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

using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GoM.Core.Mutable;
using Newtonsoft.Json;
using System.Linq;

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
            dynamic devDependencies = phpConfigFileContent[ "require-dev" ];
            List<object> projDependencies = new List<object>();

            projDependencies.AddRange( dependencies );
            projDependencies.AddRange( devDependencies );

            List<TargetDependency> allDependencies = projDependencies
                .Select( d => new TargetDependency { Name = d.ToString().Replace("\"","").Split( ':' )[ 0 ].Trim(), Version = d.ToString().Replace( "\"", "" ).Split( ':' )[ 1 ].Trim() } )
                .ToList();
            Target t = new Target{ Name = String.Empty };
            t.Dependencies.AddRange( allDependencies );
            yield return t;
        }
    }
}

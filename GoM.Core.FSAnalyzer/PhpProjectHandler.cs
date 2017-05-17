using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using GoM.Core.Abstractions;
using System.Linq;
using GoM.Core.FSAnalyzer.Utils;
using GoM.Core.Mutable;
using System.IO;

namespace GoM.Core.FSAnalyzer
{
    public class PhpProjectHandler: BaseProjectFolderHandler
    {
        public PhpProjectHandler(IFileProvider provider) 
            : base( provider )
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            var file = Files.First( f => f.Name == "composer.json" );
            PhpComposerParser pcp = new PhpComposerParser( file );
            IEnumerable<Target> targets = pcp.Read();
            Project p = new Project
            {
                Path = Path.GetDirectoryName( file.PhysicalPath )
            };
            p.Targets.AddRange( targets );
            return p;
        }
    }
}
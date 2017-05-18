using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoM.Core.Abstractions;
using GoM.Core.FSAnalyzer.Utils;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public class CsProjHandler : BaseProjectFolderHandler
    {
        public CsProjHandler(IFileProvider provider) : base(provider)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            var proj = Files.First(x => Path.GetExtension(x.PhysicalPath) == ".csproj");
            CsProjParser parser = new CsProjParser(proj);

            var targets = parser.Read();
            var project = new Project()
            {
                Path = Path.GetDirectoryName(proj.PhysicalPath),
            };

            project.Targets.AddRange(targets);
            return project;
        }
    }
}

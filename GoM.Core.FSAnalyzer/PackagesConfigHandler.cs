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
    public class PackagesConfigHandler : BaseProjectFolderHandler
    {
        public PackagesConfigHandler(IFileProvider provider, string currentPathFolder) : base(provider, currentPathFolder)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            var packageConfigFile = Files.FirstOrDefault(x => x.Name == "packages.config");
            var parser = new PackageConfigParser(packageConfigFile);
            var targets = parser.Read();
            var project = new Project()
            {
                Path = Path.GetDirectoryName(packageConfigFile.PhysicalPath)
            };
            project.Targets.AddRange(targets);
            return project;
        }
    }
}

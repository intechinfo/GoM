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
    public class CsharpProjectHandler : BaseProjectFolderHandler
    {
        public CsharpProjectHandler(IFileProvider provider) : base(provider)
        {
        }

        public override IProject Read()
        {

            var project = new Project();

            var packageConfigFile = Files.FirstOrDefault(x => x.Name == "packages.config");
            var csprojFile = Files.First(x => Path.GetExtension(x.Name) == ".csproj");
           
            if (packageConfigFile != null)
            {
                var parser = new PackageConfigParser(packageConfigFile);
                var targets = parser.Read();
                project = new Project()
                {
                    Path = Path.GetDirectoryName(packageConfigFile.PhysicalPath)
                };
                project.Targets.AddRange(targets);
            }
            else if (csprojFile != null)
            {
                var parser = new CsProjParser(csprojFile);
                var targets = parser.Read();
                project = new Project()
                {
                    Path = Path.GetDirectoryName(csprojFile.PhysicalPath)
                };
                project.Targets.AddRange(targets);
            }
            return project;
        }

        public override IProjectFolderHandler Sniff()
        {
            if (HasFile("packages.config"))
            {
                return new PackagesConfigHandler(FileProvider).Sniff();
            }
            else
            {
                return new CsProjHandler(FileProvider).Sniff();
            }
        }
    }
}

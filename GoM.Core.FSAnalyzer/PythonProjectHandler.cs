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
    public class PythonProjectHandler : BaseProjectFolderHandler
    {
        public PythonProjectHandler(IFileProvider provider, string currentPathFolder) : base(provider, currentPathFolder)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            var file = Files.First(x => x.Name == "setup.py");
            var parser = new PythonSetupParser(file);
            var targets = parser.Read();
            var project = new Project()
            {
                Path = Path.GetDirectoryName(file.PhysicalPath)
            };
            project.Targets.AddRange(targets);
            return project;
        }
    }
}

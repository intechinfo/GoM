using GoM.Core.Abstractions;
using GoM.Core.FSAnalyzer.Utils;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoM.Core.FSAnalyzer
{
    public class JsProjectHandler : BaseProjectFolderHandler
    {
        public JsProjectHandler(IFileProvider provider, string currentPathFolder) : base(provider, currentPathFolder)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            string path = this.FileProvider.GetFileInfo("./").PhysicalPath + @"\package.json";

            JsPackageParser parser = new JsPackageParser(this.FileProvider.GetFileInfo("./"));
            var targets = parser.Read();

            Project project = new Project { Path = this.FileProvider.GetFileInfo("./").PhysicalPath };
            List<Target> list = targets.ToList();

            project.Targets.AddRange(list);
            return project;
        }
    }
}

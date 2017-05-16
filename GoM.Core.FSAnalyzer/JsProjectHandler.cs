using GoM.Core.Abstractions;
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
        public JsProjectHandler(IFileProvider provider) : base(provider)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            string path = this.FileProvider.GetFileInfo("./").PhysicalPath + @"\package.json";
            string jsonFileString = File.ReadAllText(path);
            object jsonFile = JsonConvert.DeserializeObject(jsonFileString);

            Project project = new Project { Path = this.FileProvider.GetFileInfo("./").PhysicalPath };
            return project;
        }
    }
}

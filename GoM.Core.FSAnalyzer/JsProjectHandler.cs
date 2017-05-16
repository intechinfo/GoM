using GoM.Core.Abstractions;
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

        public IProject Read(string path)
        {
            string jsonFileString = File.ReadAllText(path);
            object jsonFile = JsonConvert.DeserializeObject(jsonFileString);
            return null;
        }
    }
}

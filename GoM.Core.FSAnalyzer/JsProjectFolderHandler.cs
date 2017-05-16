using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GoM.Core.FSAnalyzer
{
    public class JsProjectFolderHandler
    {
        public JsProjectFolderHandler (IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

        public IFileProvider FileProvider { get; }

        public bool Sniff()
        {
            IDirectoryContents directoryContent = FileProvider.GetDirectoryContents("./");
            return directoryContent.Any(x => Path.GetExtension(x.PhysicalPath) == "package.json");
        }

        public IProject Read(string path)
        {
            string jsonFileString = File.ReadAllText(path);
            object jsonFile = JsonConvert.DeserializeObject(jsonFileString);
            return null;
        }
    }
}

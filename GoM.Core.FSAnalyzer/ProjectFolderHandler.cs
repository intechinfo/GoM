using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GoM.Core.Abstractions;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public class ProjectFolderHandler : BaseProjectFolderHandler
    {
        public ProjectFolderHandler(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

        public override IProjectFolderHandler Sniff()
        {
            if (FileExtensions.Contains(".csproj"))
            {
                // Dispatch to CsharpHandler
            } else if (Files.Select(x => x.Name).Contains("package.json"))
            {
                // Dispatch to JsHandler   
            }
            return this;
        }

        public override IProject Read()
        {
            throw new NotImplementedException();
        }
    }
}

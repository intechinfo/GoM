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
        public ProjectFolderHandler(IFileProvider provider) : base(provider)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            if (FileExtensions.Contains(".csproj"))
            {
                return new CsharpProjectHandler(FileProvider).Sniff();
            } else if (HasFile("package.json"))
            {
                // Dispatch to JsHandler
            } else if (HasFile("setup.py"))
            {
                return new PythonProjectHandler(FileProvider).Sniff();
            }
            return this;
        }

        public override IProject Read()
        {
            return null;
        }
    }
}

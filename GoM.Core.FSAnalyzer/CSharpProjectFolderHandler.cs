using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoM.Core.Abstractions;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    [ProjectType("C#")]
    public class CSharpProjectFolderHandler : IProjectFolderHandler
    {
        public CSharpProjectFolderHandler(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

        public IFileProvider FileProvider { get; }

        public bool Sniff()
        {
            IDirectoryContents directoryContent = FileProvider.GetDirectoryContents("./");
            return directoryContent.Any(x => Path.GetExtension(x.PhysicalPath) == ".csproj");
        }

        public IProject Read()
        {
            throw new NotImplementedException();
        }
    }
}

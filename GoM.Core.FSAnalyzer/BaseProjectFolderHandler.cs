using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoM.Core.Abstractions;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public abstract class BaseProjectFolderHandler : IProjectFolderHandler
    {
        public IFileProvider FileProvider { get; internal set; }
        public IEnumerable<IFileInfo> Files => FileProvider.GetDirectoryContents("./");
        public IEnumerable<string> FileExtensions => Files.Select(x => Path.GetExtension(x.PhysicalPath));

        public virtual IProjectFolderHandler Sniff()
        {
            throw new NotImplementedException();
        }


        public virtual IProject Read()
        {
            throw new NotImplementedException();
        }
    }
}

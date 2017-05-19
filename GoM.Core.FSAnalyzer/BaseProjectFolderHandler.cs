﻿using System;
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

        public string CurrentPathFolder { get; }
        public IEnumerable<IFileInfo> Files => FileProvider.GetDirectoryContents(CurrentPathFolder);
        public IEnumerable<string> FileExtensions => Files.Select(x => Path.GetExtension(x.PhysicalPath));

        protected BaseProjectFolderHandler(IFileProvider provider, string currentPathFolder)
        {
            FileProvider = provider;
            CurrentPathFolder = currentPathFolder;

        }

        public bool HasFile(string fileName)
        {
            return Files.Select(x => x.Name).Contains(fileName);
        }

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

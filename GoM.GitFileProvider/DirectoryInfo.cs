using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace GoM.GitFileProvider
{
    class DirectoryInfo : IDirectoryContents
    {
        readonly List<IFileInfo> _files;
        public DirectoryInfo( List<IFileInfo> files)
        {
            _files = files;
        }

        public bool Exists => true;

        public IEnumerator<IFileInfo> GetEnumerator()
        {
            return _files.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

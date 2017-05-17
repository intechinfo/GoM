using LibGit2Sharp;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.GitFileProvider
{
    internal class FileInfoDirectory : IFileInfo
    {
        bool _exists;
        string _physicalPath;
        string _name;

        public FileInfoDirectory(bool exists, string physicalPath, string name)
        {
            _exists = exists;
            _physicalPath = physicalPath;
            _name = name;
        }

        public bool Exists => _exists;

        public long Length => -1;

        public string PhysicalPath => _physicalPath;

        public string Name => _name;

        public DateTimeOffset LastModified => DateTimeOffset.MinValue;

        public bool IsDirectory => true;

        public Stream CreateReadStream()
        {
            return null;
        }
    }
}

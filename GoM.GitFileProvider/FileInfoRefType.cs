using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LibGit2Sharp;

namespace GoM.GitFileProvider
{
    internal class FileInfoRefType : IFileInfo
    {
        string _physicalPath;
        string _name;

        public FileInfoRefType(string physicalPath, string name)
        {
            _physicalPath = physicalPath;
            _name = name;
        }

        public bool Exists => false;

        public long Length => -1;

        public string PhysicalPath => _physicalPath;

        public string Name => _name;

        public DateTimeOffset LastModified => default(DateTimeOffset);

        public bool IsDirectory => true;

        public Stream CreateReadStream()
        {
            throw new NotImplementedException();
        }
    }
}

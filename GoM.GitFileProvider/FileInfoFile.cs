using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GoM.GitFileProvider
{
    class FileInfoFile : IFileInfo
    {
        bool _exists;
        long _length;
        string _physicalPath;
        string _name;
        DateTimeOffset _lastModified;
        bool _isDirectory;

        public FileInfoFile(bool exists, long length, string physicalPath, string name, DateTimeOffset lastModified, bool isDirectory)
        {
            _exists = exists;
            _length = length;
            _physicalPath = physicalPath;
            _name = name;
            _lastModified = lastModified;
            _isDirectory = isDirectory;
        }

        public bool Exists => _exists;

        public long Length => _length;

        public string PhysicalPath => _physicalPath;

        public string Name => _name;

        public DateTimeOffset LastModified => _lastModified;

        public bool IsDirectory => _isDirectory;

        public Stream CreateReadStream()
        {
            throw new NotImplementedException();
        }
    }
}

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
    internal class FileInfoFile : IFileInfo
    {
        bool _exists;
        long _length;
        string _physicalPath;
        string _name;
        DateTimeOffset _lastModified;
        ReadStreamDecorator _readStreamDeco;

        public FileInfoFile(bool exists, string physicalPath, string name, DateTimeOffset lastModified, Blob file = null, RepositoryWrapper rw = null)
        {
            _exists = exists;
            _physicalPath = physicalPath;
            _name = name;
            _lastModified = lastModified;
            if (file != null)
            {
                _length = file.Size;
                _readStreamDeco = new ReadStreamDecorator(file.GetContentStream(), rw);
            }
        }

        public bool Exists => _exists;

        public long Length => _length;

        public string PhysicalPath => _physicalPath;

        public string Name => _name;

        public DateTimeOffset LastModified => _lastModified;

        public bool IsDirectory => false;

        public Stream CreateReadStream()
        {
            if (_readStreamDeco != null)
                return _readStreamDeco;           
            return null;
        }
    }
}

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
    public class FileInfoFile : IFileInfo
    {
        bool _exists;
        long _length;
        string _physicalPath;
        string _name;
        DateTimeOffset _lastModified;
        bool _isDirectory;
        Blob _file;

        public FileInfoFile(bool exists, long length, string physicalPath, string name, DateTimeOffset lastModified, bool isDirectory, Blob file = null)
        {
            _exists = exists;
            _length = length;
            _physicalPath = physicalPath;
            _name = name;
            _lastModified = lastModified;
            _isDirectory = isDirectory;
            if (isDirectory)
                _physicalPath += Path.DirectorySeparatorChar;
            _file = file;
        }

        public bool Exists => _exists;

        public long Length => _length;

        public string PhysicalPath => _physicalPath;

        public string Name => _name;

        public DateTimeOffset LastModified => _lastModified;

        public bool IsDirectory => _isDirectory;

        public Stream CreateReadStream()
        {
            if (_file != null)
                return _file.GetContentStream();           
            return null;
        }
    }
}

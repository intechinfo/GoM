using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using LibGit2Sharp;
using System.Text.RegularExpressions;

namespace GoM.GitFileProvider
{
    public class GitFileProvider : IFileProvider
    {
        readonly string _rootPath;
        readonly bool _exist;

        public GitFileProvider(string rootPath)
        {
            _rootPath = rootPath;
            _exist = IsCorrectGitDirectory();
        }

        private bool IsCorrectGitDirectory()
        {
            string fullpath = _rootPath;
            if(!Regex.IsMatch(_rootPath, @".git\\?$"))
            {
                fullpath = fullpath + @"\.git";
            }
            var dir = new DirectoryInfo(fullpath);
            if (!dir.Exists) return false;
            return true;
        }

        private void PathDecomposition(string subpath)
        {
            string[] decomposition = subpath.Split('\\');

        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            return Microsoft.Extensions.FileProviders.NotFoundDirectoryContents.Singleton;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            throw new NotImplementedException();
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }
    }
}

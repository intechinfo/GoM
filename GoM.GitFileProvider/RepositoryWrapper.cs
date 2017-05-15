using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.GitFileProvider
{
    internal class RepositoryWrapper : IDisposable
    {
        private Repository _repo;
        internal Repository Create(string rootPath)
        {
            _repo = new Repository(rootPath);
            return _repo;
        }

        public void Dispose()
        {
            _repo.Dispose();
            _repo = null;
        }
    }
}

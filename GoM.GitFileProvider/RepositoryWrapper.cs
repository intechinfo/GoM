using LibGit2Sharp;
using System;

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

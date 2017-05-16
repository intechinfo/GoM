using LibGit2Sharp;
using System;

namespace GoM.GitFileProvider
{
    internal class RepositoryWrapper : IDisposable
    {
        internal Repository Repo { get; private set; }
        internal int StreamWrapperCount { get; set; }

        public void Dispose()
        {
            if (StreamWrapperCount == 0)
            {
                Repo.Dispose();
                Repo = null;
            }
        }

        internal void Create(string rootPath)
        {
            Repo = new Repository(rootPath);
        }
    }
}

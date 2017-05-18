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

        public RepositoryWrapper(string rootPath)
        {
            if (Repo == null)
              Repo = new Repository(rootPath);
        }
    }
}

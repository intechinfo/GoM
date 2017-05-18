using LibGit2Sharp;
using System;

namespace GoM.GitFileProvider
{
    public class RepositoryWrapper : IDisposable
    {
        public Repository Repo { get; private set; }
        public int StreamWrapperCount { get; set; }

        public void Dispose()
        {
            if (StreamWrapperCount == 0)
            {
                Repo.Dispose();
                Repo = null;
            }
        }

        public void Create(string rootPath)
        {
            if (Repo == null)
              Repo = new Repository(rootPath);
        }
    }
}

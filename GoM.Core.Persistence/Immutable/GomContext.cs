using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Persistence
{
    public class GoMContext : IGoMContext
    {
        public string RootPath { get; }

        public List<BasicGitRepository> Repositories { get; } 

        public List<PackageFeed> Feeds { get; } 

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;

        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;

        public GoMContext(string rootPath)
        {
            RootPath     = rootPath;
            Repositories = new List<BasicGitRepository>();
            Feeds        = new List<PackageFeed>();
        }
    }
}

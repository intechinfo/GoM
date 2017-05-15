using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Persistence
{
    public class GoMContext : IGoMContext
    {
        public string RootPath { get; set; }

        public List<BasicGitRepository> Repositories { get; } = new List<BasicGitRepository>();

        public List<PackageFeed> Feeds { get; } = new List<PackageFeed>();

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;

        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Mutable
{
    public class GomContext : IGomContext
    {
        public string RootPath { get; set; }

        public List<BasicGitRepository> Repositories { get; } = new List<BasicGitRepository>();

        public List<PackageFeed> Feeds { get; } = new List<PackageFeed>();

        IReadOnlyCollection<IBasicGitRepository> IGomContext.Repositories => Repositories;

        IReadOnlyCollection<IPackageFeed> IGomContext.Feeds => Feeds;
    }
}

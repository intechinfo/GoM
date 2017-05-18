using GoM.Core.Immutable.Visitors;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace GoM.Core.Immutable
{
    public partial class GoMContext
    {
        public GoMContext WithAll(
            string path = null,
            ImmutableList<BasicGitRepository> repositories = null,
            ImmutableList<PackageFeed> feeds = null)
        {
            if (RootPath == path && Repositories == repositories && Feeds == feeds)
            {
                return this;
            }
            path = path == null ? RootPath : path;
            repositories = repositories == null ? Repositories : repositories;
            feeds = feeds == null ? Feeds : feeds;

            return new GoMContext(path, repositories, feeds);
        }

        public GoMContext AddOrSetGitRepositoryDetails(IGitRepository detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var basic = Repositories.FirstOrDefault(r => r.Path == detailed.Path);
            ImmutableList<BasicGitRepository> list = basic == null
                    ? Repositories.Add(BasicGitRepository.Create(detailed))
                    : Repositories.SetItem(Repositories.IndexOf(basic), BasicGitRepository.Create(detailed));
            return Create(RootPath, list, Feeds);
        }

        public GoMContext SetRepositoryDetails(GitRepository detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailRepositoryVisitor(detailed);
            return visitor.Visit(this);
        }
    }
}

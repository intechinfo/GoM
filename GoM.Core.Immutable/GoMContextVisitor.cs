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

        public GoMContext UpdateRepositoryFields(BasicGitRepository repositoryToUpdate, string newPath = null, Uri newUrl = null)
        {
            if (repositoryToUpdate == null) throw new ArgumentNullException(nameof(repositoryToUpdate));
            if (newPath == null && newUrl == null) throw new ArgumentNullException("At least one value must be not null");
            if (Repositories.Any(rep => rep.Path == newPath)) throw new ArgumentException("This path already exist");
            var visitor = new UpdateRepositoryFieldsVisitor(repositoryToUpdate, newPath, newUrl);
            return visitor.Visit(this);
        }

        public GoMContext AddOrSetRepositoryDetails(GitRepository detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailRepositoryVisitor(detailed);
            return visitor.Visit(this);
        }

        public GoMContext AddOrSetBranchDetails(BasicGitBranch branchToDetail, GitBranch detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailBranchVisitor(branchToDetail, detailed);
            return visitor.Visit(this);
        }

        public GoMContext AddOrUpdatePackageFeeds(PackageFeed feed)
        {
            var feedFound = Feeds.SingleOrDefault(f => f.Url == feed.Url);
            ImmutableList<PackageFeed> newFeeds;
            if (feedFound == null)
            {
                //Add 
                newFeeds = Feeds.Add(feed);
            }
            else
            {
                // update 
                newFeeds = Feeds.SetItem(Feeds.IndexOf(feedFound), feed);
            }
            return GoMContext.Create(this.RootPath, this.Repositories, newFeeds);
        }
    }
}

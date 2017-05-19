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

        //public GoMContext AddOrSetGitRepositoryDetails(IGitRepository detailed)
        //{
        //    if (detailed == null) throw new ArgumentNullException(nameof(detailed));
        //    var basic = Repositories.FirstOrDefault(r => r.Path == detailed.Path);
        //    ImmutableList<BasicGitRepository> list = basic == null
        //            ? Repositories.Add(BasicGitRepository.Create(detailed))
        //            : Repositories.SetItem(Repositories.IndexOf(basic), BasicGitRepository.Create(detailed));
        //    return Create(RootPath, list, Feeds);
        //}

        public GoMContext SetRepositoryDetails(GitRepository detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailRepositoryVisitor(detailed);
            return visitor.Visit(this);
        }

        public GoMContext SetBranchDetails(GitBranch detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailBranchVisitor(detailed);
            return visitor.Visit(this);
        }

        public GoMContext UpdateRepositoryFields(string path, string newPath = null, Uri newUrl = null)
        {
            if(path == null) throw new ArgumentNullException(nameof(path));
            if (newPath == null && newUrl == null) throw new ArgumentNullException("At least one value must be not null");
            if (Repositories.Any(rep => rep.Path == newPath)) throw new ArgumentException("This path already exist");
            var repositoryToUpdate = Repositories.SingleOrDefault(rep => rep.Path == path) ?? throw new ArgumentException("This repository does not exist");
            var visitor = new UpdateRepositoryFieldsVisitor(repositoryToUpdate, newPath, newUrl);
            return visitor.Visit(this);
        }

        public GoMContext AddOrUpdatePackageFeeds(PackageFeed feed)
        {
            // Chercher si le feed n'existe pas déjà basé sur l'URL. Si oui on visite si non on l'ajoute à la liste
            var feedFound = Feeds.SingleOrDefault(f => f.Url == feed.Url);
            ImmutableList<PackageFeed> newFeeds;
            if (feedFound == null)
            {
                //Add
                 newFeeds = Feeds.Add(feed);
            } else
            {
                // update
                 newFeeds = Feeds.SetItem(Feeds.IndexOf(feedFound), feed);
            }
            return GoMContext.Create(this.RootPath, this.Repositories, newFeeds);
        }
    }
}

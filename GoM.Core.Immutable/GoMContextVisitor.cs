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

        public GoMContext UpdateRepositoryFields(string path, string newPath = null, Uri newUrl = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (newPath == null && newUrl == null) throw new ArgumentNullException("At least one value must be not null");
            if (Repositories.Any(rep => rep.Path == newPath)) throw new ArgumentException("This path already exist");
            var repositoryToUpdate = Repositories.SingleOrDefault(rep => rep.Path == path) ?? throw new ArgumentException($"The with the path {path} repository does not exist");
            var visitor = new UpdateRepositoryFieldsVisitor(repositoryToUpdate, newPath, newUrl);
            return visitor.Visit(this);
        }

        public GoMContext SetRepositoryDetails(GitRepository detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailRepositoryVisitor(detailed);
            return visitor.Visit(this);
        }

        //public GoMContext 

        public GoMContext SetBranchDetails(string repositoryPath, GitBranch detailed)
        {
            if (repositoryPath == null) throw new ArgumentNullException(nameof(repositoryPath));
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var repositoryFound = Repositories.SingleOrDefault(rep => rep.Path == repositoryPath) ?? throw new ArgumentException($"The with the path {repositoryPath} repository does not exist");
            if (repositoryFound.Details == null) throw new ArgumentException($"The repository in {repositoryPath} is not detailed, it does not have branches !");
            var visitor = new DetailBranchVisitor(repositoryFound, detailed);
            return visitor.Visit(this);
        }
    }
}

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

        public GoMContext SetRepositoryDetails(BasicGitRepository reopsitoryToDetal, GitRepository detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailRepositoryVisitor(reopsitoryToDetal, detailed);
            return visitor.Visit(this);
        }

        public GoMContext SetBranchDetails(BasicGitBranch branchToDetail, GitBranch detailed)
        {
            if (detailed == null) throw new ArgumentNullException(nameof(detailed));
            var visitor = new DetailBranchVisitor(branchToDetail, detailed);
            return visitor.Visit(this);
        }

        public GoMContext UpdateBranchName(GitRepository repository, string name, string newName)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (newName == null) throw new ArgumentNullException(nameof(newName));

            var foundBranch = repository.Branches.SingleOrDefault(branch => branch.Name == name);
            if (foundBranch == null) throw new ArgumentNullException($"The branch {name} does not exist");
            if (repository.Branches.Any(branch => branch.Name == newName)) throw new ArgumentException($"The branch {newName} already exists");
            var visitor = new UpdateBrancNameVisitor(foundBranch, newName);
            return visitor.Visit(this);
        }
    }
}

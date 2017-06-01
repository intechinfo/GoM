using System;
using System.Collections.Generic;
using System.Linq;

namespace GoM.Core.Mutable
{
    public class GitRepository : IGitRepository
    {
        public GitRepository()
        {
        }

        /// <summary>
        /// Creates a Mutable GitRepository from an existing IGitRepository, ie from an Immutable GitRepository
        /// </summary>
        /// <param name="repo"></param>
        public GitRepository(IGitRepository repo)
        {
            Branches = repo is GitRepository ? (List<BasicGitBranch>)repo.Branches : new List<BasicGitBranch>(repo.Branches.Select(x => new BasicGitBranch(x)));
            Path = repo.Path;
            Url = repo.Url;
        }

        public List<BasicGitBranch> Branches { get; } = new List<BasicGitBranch>();

        public string Path { get; set; }

        public Uri Url { get; set; }

        public GitRepository Details => this;

        IGitRepository IBasicGitRepository.Details => Details;

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;
    }
}
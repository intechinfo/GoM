using GoM.Core; using System;
using System.Collections.Generic;

namespace GoM.Persistence
{
    public class GitRepository : IGitRepository
    {
        public List<BasicGitBranch> Branches { get; } = new List<BasicGitBranch>();

        public string Path { get; set; }

        public Uri Url { get; set; }

        public GitRepository Details { get; set; }

        IGitRepository IBasicGitRepository.Details => Details;

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;
    }
}
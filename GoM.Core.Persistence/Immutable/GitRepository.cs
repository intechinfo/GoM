using GoM.Core; using System;
using System.Collections.Generic;

namespace GoM.Core.Persistence
{
    public class GitRepository : IGitRepository
    {
        public List<BasicGitBranch> Branches { get; } 

        public string Path { get; }

        public Uri Url { get; }

        public GitRepository Details { get; }

        IGitRepository IBasicGitRepository.Details => Details;

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;

        public GitRepository(string path, Uri url, GitRepository details)
        {   
            Branches = new List<BasicGitBranch>();
            Path    = path;
            Url     = url;
            Details = details;
        }


    }
}
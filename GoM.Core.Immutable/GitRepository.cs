using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class GitRepository : IGitRepository
    {
        GitRepository(IGitRepository r)
        {
            Debug.Assert(!(r is GitRepository));
            Path = r.Path;
            Url = r.Url;
        }

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => throw new NotImplementedException();

        public string Path { get; }

        public Uri Url { get; }

        public GitRepository Details => this;

        IGitRepository IBasicGitRepository.Details => Details;

        public static GitRepository Create(IGitRepository r) => r as GitRepository ?? new GitRepository(r);

    }
}
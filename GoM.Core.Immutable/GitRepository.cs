using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class GitRepository : IGitRepository
    {
        GitRepository(string path, Uri url)
        {
            Path = path ?? throw new ArgumentException(nameof(path));
            Url = url ?? throw new ArgumentException(nameof(url));
        }

        GitRepository(IGitRepository r)
        {
            Debug.Assert(!(r is GitRepository));
            Path = r.Path ?? throw new ArgumentException(nameof(r.Path));
            Url = r.Url ?? throw new ArgumentException(nameof(r.Url));
        }

        public ImmutableList<BasicGitBranch> Branches { get; } = ImmutableList.Create<BasicGitBranch>();

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;

        public string Path { get; }

        public Uri Url { get; }

        public GitRepository Details => this;

        IGitRepository IBasicGitRepository.Details => Details;

        public static GitRepository Create(IGitRepository r) => r as GitRepository ?? new GitRepository(r);

        public static GitRepository Create(string path, Uri url)
        {
            return new GitRepository(path, url);
        }
    }
}
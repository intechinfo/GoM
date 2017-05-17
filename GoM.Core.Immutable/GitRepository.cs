﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class GitRepository : IGitRepository
    {
        GitRepository(string path, Uri url, ImmutableList<BasicGitBranch> branches =  null)
        {
            Path = path ?? throw new ArgumentException(nameof(path));
            Url = url ?? throw new ArgumentException(nameof(url));
            Branches = branches ?? ImmutableList.Create<BasicGitBranch>();
        }

        GitRepository(IGitRepository repository)
        {
            Debug.Assert(!(repository is GitRepository));
            Path = repository.Path ?? throw new ArgumentException(nameof(repository.Path));
            Url = repository.Url ?? throw new ArgumentException(nameof(repository.Url));
            Branches = (ImmutableList<BasicGitBranch>)repository.Branches ?? ImmutableList.Create<BasicGitBranch>();
        }

        public ImmutableList<BasicGitBranch> Branches { get; } = ImmutableList.Create<BasicGitBranch>();

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;

        public string Path { get; }

        public Uri Url { get; }

        public GitRepository Details => this;

        IGitRepository IBasicGitRepository.Details => Details;

        public static GitRepository Create(IGitRepository r) => r as GitRepository ?? new GitRepository(r);

        public static GitRepository Create(string path, Uri url, ImmutableList<BasicGitBranch> branches = null) => new GitRepository(path, url, branches);
    }
}
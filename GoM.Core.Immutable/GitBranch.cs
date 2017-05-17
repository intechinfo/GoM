using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class GitBranch : IGitBranch
    {
        GitBranch(string name, IBranchVersionInfo version = null)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Version = version != null ? BranchVersionInfo.Create(version) : null;
        }

        GitBranch(IGitBranch branch)
        {
            Debug.Assert(!(branch is GitBranch));
            Projects = (ImmutableList<BasicProject>)branch.Projects ?? ImmutableList.Create<BasicProject>();
            Version = branch.Version != null ? BranchVersionInfo.Create(branch.Version) : null;
        }

        public ImmutableList<BasicProject> Projects { get; } = ImmutableList.Create<BasicProject>();

        public BranchVersionInfo Version { get; }

        internal static GitBranch Create(IGitBranch details) => details as GitBranch ?? new GitBranch(details);

        public string Name { get; }

        internal static GitBranch Create(string name, BranchVersionInfo version = null)
        {
            return new GitBranch(name, version);
        }

        public GitBranch Details => this;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;
    }
}
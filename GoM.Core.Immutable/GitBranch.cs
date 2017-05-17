using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GoM.Core.Immutable
{
    public class GitBranch : IGitBranch
    {
        public ImmutableList<BasicProject> Projects { get; } = ImmutableList.Create<BasicProject>();

        public BranchVersionInfo Version { get; }

        public string Name { get; }

        internal static GitBranch Create()
        {
            throw new NotImplementedException();
        }

        public GitBranch Details => this;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;
    }
}
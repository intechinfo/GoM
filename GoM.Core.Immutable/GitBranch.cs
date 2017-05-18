using System;
using System.Collections.Generic;

namespace GoM.Core.Immutable
{
    public class GitBranch : IGitBranch
    {
        public IReadOnlyCollection<IBasicProject> Projects => throw new NotImplementedException();

        public BranchVersionInfo Version { get; }

        public string Name { get; }

        internal static GitBranch Create()
        {
            throw new NotImplementedException();
        }

        public GitBranch Details => this;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;
    }
}
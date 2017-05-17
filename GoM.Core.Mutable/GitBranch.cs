using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoM.Core.Mutable
{
    public class GitBranch : IGitBranch
    {
        public List<BasicProject> Projects { get; } = new List<BasicProject>();

        public BranchVersionInfo Version { get; set; }

        public string Name { get; set; }

        public GitBranch Details => this;

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;
    }
}
using GoM.Core; using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoM.Persistence
{
    public class GitBranch : IGitBranch
    {
        public List<Project> Projects { get; } = new List<Project>();

        public BranchVersionInfo Version { get; set; }

        public string Name { get; set; }

        public GitBranch Details { get; set; }

        IReadOnlyCollection<IProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;
    }
}
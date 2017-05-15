using GoM.Core; using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GoM.Persistence
{
    public class GitBranch : IGitBranch
    {
        public List<Project> Projects { get; }

        public BranchVersionInfo Version { get; }

        public string Name { get; }

        public GitBranch Details { get; }

        IReadOnlyCollection<IProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;


        public GitBranch(BranchVersionInfo version, string name, GitBranch details )
        {
            Projects = new List<Project>();
            Version = version;
            Name    = name;
            Details = details;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace GoM.Core.Mutable
{
    public class GitBranch : IGitBranch
    {
        public GitBranch()
        {
        }

        /// <summary>
        /// Creates a Mutable GitBranch from an existing IGitBranch, ie from an Immutable GitBranch
        /// </summary>
        /// <param name="branch"></param>
        public GitBranch(IGitBranch branch)
        {
            Projects = branch is GitBranch ? (List<BasicProject>)branch.Projects : new List<BasicProject>(branch.Projects.Select(x => new BasicProject(x)));
            Version = branch is GitBranch ? (BranchVersionInfo)branch.Version : new BranchVersionInfo(branch.Version);
            Name = branch.Name;
        }

        public List<BasicProject> Projects { get; } = new List<BasicProject>();

        public BranchVersionInfo Version { get; set; }

        public string Name { get; set; }

        public GitBranch Details => this;

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;
    }
}
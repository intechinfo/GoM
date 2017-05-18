using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Mutable
{
    public class BasicGitBranch : IBasicGitBranch
    {
        public BasicGitBranch()
        {
        }

        /// <summary>
        /// Creates a Mutable BasicGitBranch from an existing IBasicGitBranch, ie from an Immutable BasicGitBranch
        /// </summary>
        /// <param name="branch"></param>
        public BasicGitBranch(IBasicGitBranch branch)
        {
            Name = branch.Name;
            Details = branch is BasicGitBranch ? (GitBranch)branch.Details : new GitBranch(branch.Details);
        }
        public string Name { get; set; }

        public GitBranch Details { get; set; }

        IGitBranch IBasicGitBranch.Details => Details;
    }
}

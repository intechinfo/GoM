using GoM.Core; using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Persistence
{
    public class BasicGitBranch : IBasicGitBranch
    {
        public string Name { get; }

        public GitBranch Details { get; }

        IGitBranch IBasicGitBranch.Details => Details;



        public BasicGitBranch(string name, GitBranch details)
        {
            Name    = name;
            Details = details;
        }
    }
}

using GoM.Core; using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Persistence
{
    public class BasicGitBranch : IBasicGitBranch
    {
        public string Name { get; set; }

        public GitBranch Details { get; set; }

        IGitBranch IBasicGitBranch.Details => Details;
    }
}

using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicGitBranch : IBasicGitBranch
    {
        private XElement t;

        public BasicGitBranch ( XElement t )
        {
            this.t = t;
            Name = t.Attribute( nameof( Name ) ).Value;
            Details = new GitBranch( t.Element( nameof( Details ) ) );
        }

        public string Name { get; set; }

        public GitBranch Details { get; set; }

        IGitBranch IBasicGitBranch.Details => Details;
    }
}

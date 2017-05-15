using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicGitBranch : IBasicGitBranch
    {
        public string Name { get; }
        private XElement t;

        public BasicGitBranch ( XElement t )
        {
            this.t = t;
            Name = t.Attribute( nameof( Name ) ).Value;
            Details = new GitBranch( t.Element( nameof( Details ) ) );
        }


        public GitBranch Details { get; }

        IGitBranch IBasicGitBranch.Details => Details;



        public BasicGitBranch(string name, GitBranch details)
        {
            Name    = name;
            Details = details;
        }
    }
}

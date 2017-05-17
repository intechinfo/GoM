using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicGitBranch : IBasicGitBranch
    {

        public const string BASIC_GIT_BRANCH = "basicGitBranch";
        public const string BASIC_GIT_BRANCH_NAME = "name";

        public string Name { get; }
        private XElement t;

        public BasicGitBranch ( XElement t )
        {
            this.t = t;
            Name = t.Attribute( BASIC_GIT_BRANCH_NAME ).Value;

            var node = t.Element( BASIC_GIT_BRANCH_NAME );
            Details = node != null ? new GitBranch( node ) : null;
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

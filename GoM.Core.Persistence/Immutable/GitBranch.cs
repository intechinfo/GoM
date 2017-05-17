using GoM.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class GitBranch : IGitBranch
    {
        public const string GIT_BRANCH = "gitBranch";
        public const string GIT_BRANCH_NAME = "name";


        private XElement xElement;
        public List<BasicProject> Projects { get; }

        public GitBranch ( XElement xElement )
        {
            this.xElement = xElement;
            Projects = new List<BasicProject>();

            foreach ( var t in xElement.Elements( Project.PROJECT ) )
            {
                Projects.Add( new BasicProject( t ) );
            }

            Version = new BranchVersionInfo( xElement.Element( BranchVersionInfo.BRANCH_VERSION_INFO ) );
            Name = xElement.Attribute( GIT_BRANCH ).Value;

        }

        public BranchVersionInfo Version { get; }

        public string Name { get; }

        public GitBranch Details => this;

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;


        public GitBranch(BranchVersionInfo version, string name, GitBranch details )
        {
            Projects = new List<BasicProject>();
            Version = version;
            Name    = name;
        }
    }
}
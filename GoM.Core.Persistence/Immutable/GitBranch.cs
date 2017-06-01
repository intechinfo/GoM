using GoM.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class GitBranch : IGitBranch
    {
        private XElement xElement;
        public List<BasicProject> Projects { get; }

        public GitBranch ( XElement xElement )
        {
            this.xElement = xElement;
            Projects = new List<BasicProject>();
            foreach(var t in xElement.Elements(typeof(IProject).Name))
            {
                Projects.Add( new BasicProject( t ) );
            }

            Version = new BranchVersionInfo( xElement.Element( typeof( IBranchVersionInfo ).Name ) );
            Name = xElement.Attribute( nameof( Name ) ).Value;
            Details = new GitBranch( xElement.Element( nameof( GitBranch ) ) );

        }


        public BranchVersionInfo Version { get; }

        public string Name { get; }

        public GitBranch Details { get; }

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;


        public GitBranch(BranchVersionInfo version, string name, GitBranch details )
        {
            Projects = new List<BasicProject>();
            Version = version;
            Name    = name;
            Details = details;
        }
    }
}
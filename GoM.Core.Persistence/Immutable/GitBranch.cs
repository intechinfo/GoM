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
        public List<Project> Projects { get; }

        public GitBranch ( XElement xElement )
        {
            this.xElement = xElement;
            Projects = new List<Project>();
            foreach(var t in xElement.Elements(nameof(Projects)))
            {
                Projects.Add( new Project( t ) );
            }

            Version = new BranchVersionInfo( xElement.Element( nameof( Version ) ) );
            Name = xElement.Attribute( nameof( Name ) ).Value;
            Details = new GitBranch( xElement.Element( nameof( GitBranch ) ) );

        }


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
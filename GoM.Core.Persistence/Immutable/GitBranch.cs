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

        public List<Project> Projects { get; } = new List<Project>();

        public BranchVersionInfo Version { get; set; }

        public string Name { get; set; }

        public GitBranch Details { get; set; }

        IReadOnlyCollection<IProject> IGitBranch.Projects => Projects;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;
    }
}
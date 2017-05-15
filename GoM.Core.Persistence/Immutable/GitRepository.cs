using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class GitRepository : IGitRepository
    {
        private XElement xElement;

        public GitRepository ( XElement xElement )
        {
            this.xElement = xElement;

            Branches = new List<BasicGitBranch>();
            foreach(var node in xElement.Elements(nameof(Branches)))
            {
                Branches.Add( new BasicGitBranch( node ) );
            }

            Path = xElement.Attribute( nameof( Path ) ).Value;
            Url = new Uri(xElement.Attribute(nameof(Url)).Value);
            Details = new GitRepository( xElement.Element( nameof( Details ) ) );


        }

        public List<BasicGitBranch> Branches { get; } = new List<BasicGitBranch>();

        public string Path { get; set; }

        public Uri Url { get; set; }

        public GitRepository Details { get; set; }

        IGitRepository IBasicGitRepository.Details => Details;

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;
    }
}
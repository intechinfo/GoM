using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class GitRepository : IGitRepository
    {
        private XElement xElement;
        public List<BasicGitBranch> Branches { get; } 

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
        }
        public GitRepository ( string path, Uri url )
        {
            Branches = new List<BasicGitBranch>();
            Path = path;
            Url = url;
        }


        public string Path { get; }

        public Uri Url { get; }

        IGitRepository IBasicGitRepository.Details => this;

        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => Branches;

        


    }
}
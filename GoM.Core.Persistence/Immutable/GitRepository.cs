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
            foreach(var node in xElement.Elements(GoMAttributeNamesV1.BASIC_GIT_BRANCH))    
            {
                Branches.Add( new BasicGitBranch( node ) );
            }

            Path = xElement.Attribute( GoMAttributeNamesV1.GIT_REPOSITORY_PATH ).Value;
            Url = new Uri(xElement.Attribute( GoMAttributeNamesV1.GIT_REPOSITORY_URL ).Value);
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
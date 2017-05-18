using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace GoM.Core.Persistence
{
    public class GitRepository : IGitRepository
    {

        public const string GIT_REPOSITORY = "gitRepository";
        public const string GIT_REPOSITORY_PATH = "path";
        public const string GIT_REPOSITORY_URL = "url";


        private XElement xElement;
        public List<BasicGitBranch> Branches { get; } 

        public GitRepository ( XElement xElement )
        {
            this.xElement = xElement;

            Branches = new List<BasicGitBranch>();
            Branches = xElement.Elements(BasicGitBranch.BASIC_GIT_BRANCH).Select(node => new BasicGitBranch(node)).ToList();
            //foreach (var node in xElement.Elements(BasicGitBranch.BASIC_GIT_BRANCH))    
            //{
            //    Branches.Add( new BasicGitBranch( node ) );
            //}

            Path = xElement.Attribute( GIT_REPOSITORY_PATH ).Value;
            Url = new Uri(xElement.Attribute( GIT_REPOSITORY_URL ).Value);
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
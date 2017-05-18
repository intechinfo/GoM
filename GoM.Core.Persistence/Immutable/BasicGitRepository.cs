using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicGitRepository : IBasicGitRepository
    {
        public const string BASIC_GIT_REPOSITORY = "basicGitRepository";
        public const string BASIC_GIT_REPOSITORY_PATH = "path";
        public const string BASIC_GIT_REPOSITORY_URL = "url";


        private XElement el;
        public string Path { get; }

        public BasicGitRepository ( XElement el )
        {
            this.el = el;
            Path = el.Attribute( BASIC_GIT_REPOSITORY_PATH ).Value;
            Url = new Uri( el.Attribute(BASIC_GIT_REPOSITORY_URL).Value);

            // c=========================================================================3
            var node = el.Element( GitRepository.GIT_REPOSITORY );
            Details = node != null ? new GitRepository(node) : null;
        }


        public Uri Url { get; }

        public GitRepository Details { get; }

        IGitRepository IBasicGitRepository.Details => Details;


        public BasicGitRepository(string path, Uri url, GitRepository details)
        {
            Path    = path;
            Url     = url;
            Details = details;
        }
    }
}
using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicGitRepository : IBasicGitRepository
    {
        private XElement el;
        public string Path { get; }

        public BasicGitRepository ( XElement el )
        {
            this.el = el;
            Path = el.Attribute( GoMAttributeNamesV1.BASIC_GIT_REPOSITORY_PATH ).Value;
            Url = new Uri( GoMAttributeNamesV1.BASIC_GIT_REPOSITORY_URL);

            // c=========================================================================3
            var node = el.Element( GoMAttributeNamesV1.GIT_REPOSITORY );
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
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
            Path = el.Attribute( nameof( Path ) ).Value;
            Url = new Uri(el.Attribute( nameof( Url ) ).Value);

            Details = new GitRepository(el.Element(nameof(Details)));
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
using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicGitRepository : IBasicGitRepository
    {
        private XElement el;

        public BasicGitRepository ( XElement el )
        {
            this.el = el;
            Path = el.Attribute( nameof( Path ) ).Value;
            Url = new Uri(el.Attribute( nameof( Url ) ).Value);

            Details = new GitRepository(el.Element(nameof(Details)));
        }

        public string Path { get; set; }

        public Uri Url { get; set; }

        public GitRepository Details { get; set; }

        IGitRepository IBasicGitRepository.Details => Details;
    }
}
using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class GoMContext : IGoMContext
    {
        private XElement root;

        public GoMContext ( XElement root )
        {
            this.root = root;
            Repositories = new List<BasicGitRepository>();
            foreach(var el in root.Elements( typeof( BasicGitRepository ).Name ) )
            {
                Repositories.Add( new BasicGitRepository( el ) );
            }

            Feeds = new List<PackageFeed>();
            foreach ( var el in root.Elements( typeof( PackageFeed ).Name ) )
            {
                Feeds.Add( new PackageFeed( el ) );
            }

        }

        public string RootPath { get; set; }

        public List<BasicGitRepository> Repositories { get; } = new List<BasicGitRepository>();

        public List<PackageFeed> Feeds { get; } = new List<PackageFeed>();

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;

        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;
    }
}

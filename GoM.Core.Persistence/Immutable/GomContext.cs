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
        public string RootPath { get; }

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


        public List<BasicGitRepository> Repositories { get; } 

        public List<PackageFeed> Feeds { get; } 

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;

        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;

        public GoMContext(string rootPath, List<BasicGitRepository> repo, List<PackageFeed> feed)
        {
            RootPath     = rootPath;
            Repositories = new List<BasicGitRepository>();
            Feeds        = new List<PackageFeed>();
        }
    }
}

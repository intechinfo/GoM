using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace GoM.Core.Persistence
{
    public class GoMContext : IGoMContext
    {
        public const string GOM_CONTEXT = "gomContext";
        public const string GOM_CONTEXT_ROOTPATH = "rootPath";


        private XElement root;
        public string RootPath { get; }

        public GoMContext ( XElement root )
        {
            this.root = root;
            Repositories = new List<BasicGitRepository>();
            Repositories = root.Elements(BasicGitRepository.BASIC_GIT_REPOSITORY).Select(el => new BasicGitRepository(el)).ToList();
            //foreach (var el in root.Elements( BasicGitRepository.BASIC_GIT_REPOSITORY ) )
            //{
            //    Repositories.Add( new BasicGitRepository( el ) );
            //}

            Feeds = new List<PackageFeed>();
            Feeds = root.Elements(PackageFeed.PACKAGE_FEED).Select(el => new PackageFeed(el)).ToList();
            //foreach ( var el in root.Elements( PackageFeed.PACKAGE_FEED) )
            //{
            //    Feeds.Add( new PackageFeed( el ) );
            //}

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

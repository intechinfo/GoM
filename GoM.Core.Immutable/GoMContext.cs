using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class GoMContext : IGoMContext
    {
        private GoMContext(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            RootPath = path ?? throw new ArgumentException("path must not be null");
            Repositories = repositories ?? throw new ArgumentException("repositories must not be null");
            Feeds = feeds ?? throw new ArgumentException("feeds must not be null");
        }

        private GoMContext(string path)
        {
            RootPath = path;
        }

        private GoMContext(IGoMContext context)
        {
            RootPath = context.RootPath;
            Repositories = (ImmutableList<BasicGitRepository>)context.Repositories;
            Feeds = (ImmutableList<PackageFeed>)context.Feeds;
        }

        public string RootPath { get; }

        public ImmutableList<BasicGitRepository> Repositories { get; }

        public ImmutableList<PackageFeed> Feeds { get; }

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;
        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;

        public static GoMContext Create(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            return new GoMContext(path, repositories, feeds);
        }

        public static GoMContext Create(IGoMContext context)
        {
            return new GoMContext(context);
        }

        public GoMContext WithAll(
            string path = null, 
            ImmutableList<BasicGitRepository> repositories = null, 
            ImmutableList<PackageFeed> feeds = null)
        {
            if (RootPath == path && Repositories == repositories && Feeds == feeds)
            {
                return this;
            }
            path = path == null ? RootPath : path;
            repositories = repositories == null ? Repositories : repositories;
            feeds = feeds == null ? Feeds : feeds;

            return new GoMContext(path, repositories, feeds);
        }

        public GoMContext UpdateRepository(BasicGitRepository repoToUpdate, string path = null, Uri url = null, GitRepository details = null)
        {
            var tmpRepository = repoToUpdate.WithAll(path, url, details);
            var tmpRepositories = Repositories.SetItem(this.Repositories.IndexOf(repoToUpdate), tmpRepository);
            return new GoMContext(RootPath, tmpRepositories, Feeds);
        }

        public GoMContext AddRepository(BasicGitRepository repoToAdd)
        {
            var tmpRepositories = Repositories.Add(repoToAdd);
            return new GoMContext(RootPath, tmpRepositories, Feeds);
        }

        public GoMContext RemoveRepository(BasicGitRepository repoToRemove)
        {
            var tmpRepositories = Repositories.Remove(repoToRemove);
            return new GoMContext(RootPath, tmpRepositories, Feeds);
        }

        public GoMContext AddOrSetGitRepositoryDetails(IGitRepository detailed)
        {
            if (detailed != null) throw new ArgumentNullException(nameof(detailed));
            var basic = Repositories.FirstOrDefault(r => r.Path == detailed.Path);
            ImmutableList<BasicGitRepository> list = basic == null
                    ? Repositories.Add(BasicGitRepository.Create(detailed))
                    : Repositories.SetItem(Repositories.IndexOf(basic), BasicGitRepository.Create(detailed));
            return Create(RootPath, list, Feeds);
        }

        //public GoMContext AddOrSetGitBranchDetails( GitRepository repository, IGitBranch detailed)
        //{
        //    Visitor v = new 
        //}

        public class ToUppercaseVisitor : Visitor
        {
            readonly string _pattern;

            public ToUppercaseVisitor( string pattern )
            {
                _pattern = pattern;
            }

            public override GoMContext Visit(GoMContext c)
            {
                if( !c.RootPath.All( x => Char.IsUpper(x)) && c.RootPath.Contains(_pattern) )
                {
                    c = c.WithAll(c.RootPath.ToUpperInvariant(), c.Repositories, c.Feeds);
                }
                return base.Visit(c);
            }

            protected override BasicGitRepository Visit(BasicGitRepository r)
            {
                if (!r.Path.All(x => Char.IsUpper(x)) && r.Path.Contains(_pattern))
                {
                    r = r.WithAll(r.Path.ToUpperInvariant(), r.Url, r.Details );
                }
                return base.Visit(r);
            }

            protected override GitRepository Visit(GitRepository r)
            {
                return base.Visit(r);
            }

        }

    }
}

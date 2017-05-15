using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GoM.Core.Immutable
{
    public class GoMContext : IGoMContext
    {
        private readonly string _rootPath;

        private readonly ImmutableList<BasicGitRepository> _repositories = ImmutableList.Create<BasicGitRepository>();

        private readonly ImmutableList<PackageFeed> _feeds = ImmutableList.Create<PackageFeed>();

        private GoMContext(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            _rootPath = path;
            _repositories = repositories;
            _feeds = feeds;
        }

        private GoMContext(string path)
        {
            _rootPath = path;
        }

        private GoMContext(IGoMContext context)
        {
            _rootPath = context.RootPath;
            _repositories = (ImmutableList<BasicGitRepository>)context.Repositories;
            _feeds = (ImmutableList<PackageFeed>)context.Feeds;
        }

        public GoMContext WithAll(string path = null, ImmutableList<BasicGitRepository> repositories = null, ImmutableList<PackageFeed> feeds = null)
        {
            if(_rootPath == path && _repositories == repositories && _feeds == feeds)
            {
                return this;
            }
            path = path == null ? _rootPath : path;
            repositories = repositories == null ? _repositories : repositories;
            feeds = feeds == null ? _feeds : feeds;

            return new GoMContext(path, repositories, feeds);

        }

        public string RootPath => _rootPath;

        public ImmutableList<BasicGitRepository> Repositories => _repositories;

        public ImmutableList<PackageFeed> Feeds => _feeds;

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => _repositories;
        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => _feeds;

    }
}

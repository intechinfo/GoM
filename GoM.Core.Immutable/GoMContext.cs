using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class GoMContext : IGoMContext
    {
        private readonly string _rootPath;

        private readonly ImmutableList<BasicGitRepository> _repositories = ImmutableList.Create<BasicGitRepository>();

        private readonly ImmutableList<PackageFeed> _feeds = ImmutableList.Create<PackageFeed>();

        private GoMContext(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            _rootPath = path ?? throw new ArgumentException("path must not be null");
            _repositories = repositories ?? throw new ArgumentException("repositories must not be null");
            _feeds = feeds ?? throw new ArgumentException("feeds must not be null");
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

        public string RootPath => _rootPath;

        public ImmutableList<BasicGitRepository> Repositories => _repositories;

        public ImmutableList<PackageFeed> Feeds => _feeds;

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => _repositories;
        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => _feeds;

        public static GoMContext Create(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            return new GoMContext(path, repositories, feeds);
        }

        public GoMContext WithAll(string path = null, ImmutableList<BasicGitRepository> repositories = null, ImmutableList<PackageFeed> feeds = null)
        {
            if (_rootPath == path && _repositories == repositories && _feeds == feeds)
            {
                return this;
            }
            path = path == null ? _rootPath : path;
            repositories = repositories == null ? _repositories : repositories;
            feeds = feeds == null ? _feeds : feeds;

            return new GoMContext(path, repositories, feeds);
        }

        public GoMContext UpdateRepository(BasicGitRepository repoToUpdate, string path = null, Uri url = null, GitRepository details = null)
        {
            var tmpRepository = repoToUpdate.WithAll(path, url, details);
            var tmpRepositories = _repositories.SetItem(this.Repositories.IndexOf(repoToUpdate), tmpRepository);
            return new GoMContext(_rootPath, tmpRepositories, _feeds);
        }

        public GoMContext AddRepository(BasicGitRepository repoToAdd)
        {
            var tmpRepositories = _repositories.Add(repoToAdd);
            return new GoMContext(_rootPath, tmpRepositories, _feeds);
        }

        public GoMContext RemoveRepository(BasicGitRepository repoToRemove)
        {
            var tmpRepositories = _repositories.Remove(repoToRemove);
            return new GoMContext(_rootPath, tmpRepositories, _feeds);
        }

    }
}

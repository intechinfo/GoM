using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoM.Core.Immutable
{
    public partial class GoMContext : IGoMContext
    {
        private GoMContext(string path)
        {
            RootPath = path ?? throw new ArgumentException(nameof(path));
        }

        private GoMContext(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            RootPath = path ?? throw new ArgumentException(nameof(path));
            Repositories = repositories ?? throw new ArgumentException(nameof(repositories));
            Feeds = feeds ?? throw new ArgumentException(nameof(feeds));

            if (Repositories.Any(rep => rep == null)) throw new ArgumentException($"A repository in {nameof(repositories)} is null");
            if (Feeds.Any(feed => feed == null)) throw new ArgumentException($"A feed in {nameof(feeds)} is null");

            // Check duplicates on repositories(path) and feeds (url)
            if (CheckDuplicates(repositories, feeds)) throw new ArgumentException("Duplicate package feeds or repositories found");
        }

        private GoMContext(IGoMContext context)
        {
            RootPath = context.RootPath;
            Repositories = ImmutableList.Create(context.Repositories.Select( x => BasicGitRepository.Create(x)).ToArray());
            Feeds = context.Feeds == null ? ImmutableList.Create<PackageFeed>() : ImmutableList.Create(context.Feeds.Select(x => PackageFeed.Create(x)).ToArray());

            // Check duplicates on repositories(path) and feeds (url)
            if (CheckDuplicates(Repositories, Feeds)) throw new ArgumentException("Duplicate package feeds or repositories found");
        }

        public string RootPath { get; }

        public ImmutableList<BasicGitRepository> Repositories { get; } = ImmutableList.Create<BasicGitRepository>();

        public ImmutableList<PackageFeed> Feeds { get; } = ImmutableList.Create<PackageFeed>();

        IReadOnlyCollection<IBasicGitRepository> IGoMContext.Repositories => Repositories;
        IReadOnlyCollection<IPackageFeed> IGoMContext.Feeds => Feeds;

        public static GoMContext Create(string path)
        {
            return new GoMContext(path);
        }
        public static GoMContext Create(string path, ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            return new GoMContext(path, repositories, feeds);
        }

        public static GoMContext Create(IGoMContext context) => context as GoMContext ?? new GoMContext(context);

        bool CheckDuplicates(ImmutableList<BasicGitRepository> repositories, ImmutableList<PackageFeed> feeds)
        {
            bool isDuplicates = false;
            isDuplicates = repositories.Distinct(
                EqualityComparerGenerator.CreateEqualityComparer<BasicGitRepository>((x, y) => x.Path == y.Path, x => x.Path.GetHashCode())
            ).Count() < repositories.Count;
            isDuplicates = isDuplicates ? true : feeds.Distinct(
                EqualityComparerGenerator.CreateEqualityComparer<PackageFeed>((x, y) => x.Url.OriginalString == y.Url.OriginalString, x => x.Url.OriginalString.GetHashCode())
            ).Count() < feeds.Count;
            return isDuplicates;
        }
    }
}

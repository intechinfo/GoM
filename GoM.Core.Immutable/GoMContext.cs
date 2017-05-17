﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class GoMContext : IGoMContext
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

            if (Repositories.Any(rep => rep != null)) throw new ArgumentException($"A repository in {nameof(repositories)} is null");
            if (Feeds.Any(feed => feed != null)) throw new ArgumentException($"A feed in {nameof(feeds)} is null");

            // Check duplicates on repositories(path && url) and feeds (url)
            bool isDuplicates = false;
            //isDuplicates = repositories.Distinct()
        }

        private GoMContext(IGoMContext context)
        {
            RootPath = context.RootPath;
            Repositories = (ImmutableList<BasicGitRepository>)context.Repositories;
            Feeds = (ImmutableList<PackageFeed>)context.Feeds;
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
    }
}
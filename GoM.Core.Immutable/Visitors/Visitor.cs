using System;
using System.Collections.Immutable;

namespace GoM.Core.Immutable.Visitors
{
    public class Visitor
    {
        public virtual GoMContext Visit(GoMContext c)
        {
            var repos = Visit(c.Repositories, Visit);
            var feeds = Visit(c.Feeds, Visit);
            if (repos != c.Repositories
                || feeds != c.Feeds)
            {
                return GoMContext.Create(c.RootPath, repos, feeds);
            }
            return c;
        }

        public virtual BasicGitRepository Visit(BasicGitRepository basicRepository)
        {
            var visitedDetails = basicRepository.Details != null ? Visit(basicRepository.Details) : null;
            return visitedDetails != basicRepository.Details
                    ? (visitedDetails == null ? BasicGitRepository.Create(basicRepository.Path, basicRepository.Url) : BasicGitRepository.Create(visitedDetails))
                    : basicRepository;
        }

        public virtual GitRepository Visit(GitRepository repository)
        {
            var visitedBranches = repository.Branches != null ? Visit(repository.Branches, Visit) : ImmutableList.Create<BasicGitBranch>();
            return visitedBranches != repository.Branches ? GitRepository.Create(repository.Path, repository.Url, visitedBranches) : repository;
        }

        public virtual BasicGitBranch Visit(BasicGitBranch basicBranch)
        {
            return basicBranch;
        }

        public virtual PackageFeed Visit(PackageFeed p)
        {
            return p;
        }

        static ImmutableList<T> Visit<T>(ImmutableList<T> input, Func<T, T> transformer) where T : class
        {
            int i = 0;
            ImmutableList<T>.Builder listBuilder = null;
            foreach (var element in input)
            {
                T visitedElement = transformer(element);
                if (visitedElement != element && listBuilder == null)
                {
                    listBuilder = ImmutableList.CreateBuilder<T>();
                    listBuilder.AddRange(input.GetRange(0, i));
                }
                if (listBuilder != null && visitedElement != null) listBuilder.Add(visitedElement);
                ++i;
            }
            return listBuilder == null ? input : listBuilder.ToImmutableList();
        }

    }
}

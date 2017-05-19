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
            var visitedBranchDetails = basicBranch.Details != null ? Visit(basicBranch.Details) : null;
            return visitedBranchDetails != basicBranch.Details
                ? (visitedBranchDetails == null ? BasicGitBranch.Create(basicBranch.Name) : BasicGitBranch.Create(visitedBranchDetails))
                : basicBranch;
        }

        public virtual GitBranch Visit(GitBranch branch)
        {
            var visitedProjects = branch.Projects != null ? Visit(branch.Projects, Visit) : ImmutableList.Create<BasicProject>();
            return visitedProjects != branch.Projects ? GitBranch.Create(branch.Name, branch.Version, visitedProjects) : branch;
        }

        public virtual BasicProject Visit(BasicProject basicProject)
        {
            var visitedProjectDetails = basicProject.Details != null ? Visit(basicProject.Details) : null;
            return visitedProjectDetails != basicProject.Details
                ? (visitedProjectDetails == null ? BasicProject.Create(basicProject.Path) : BasicProject.Create(visitedProjectDetails))
                : basicProject;
        }

        public virtual Project Visit(Project project)
        {
            var visitedTargets = project.Targets != null ? Visit(project.Targets, Visit) : ImmutableList.Create<Target>();
            return visitedTargets != project.Targets ? Project.Create(project.Path, visitedTargets) : project;
        }

        public virtual Target Visit(Target target)
        {
            var visitedDependencies = target.Dependencies != null ? Visit(target.Dependencies, Visit) : null;
            return visitedDependencies != target.Dependencies
            ? (visitedDependencies == null ? Target.Create(target.Name) : Target.Create(target.Name, visitedDependencies))
            : target;
        }

        public virtual TargetDependency Visit(TargetDependency targetDependency) => targetDependency;
        
        public virtual PackageFeed Visit(PackageFeed feed)
        {
            var visitedPackageInstances = feed.Packages != null ? Visit(feed.Packages, Visit) : null;
            return visitedPackageInstances != feed.Packages
            ? (visitedPackageInstances == null ? throw new ArgumentNullException(nameof(visitedPackageInstances)) : PackageFeed.Create(feed.Url, visitedPackageInstances))
            : feed;
        }

        public virtual PackageInstance Visit(PackageInstance package) => package;

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

using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Xunit;

namespace GoM.Core.Immutable.Tests
{
    public class VisitorTests
    {
        private GoMContext CreateTestGoMContext()
        {
            // BranchVersionInfo
            Immutable.VersionTag newVersionTag = VersionTag.Create("Version 1.0.0");
            Immutable.BranchVersionInfo newBranchVersionInfo = BranchVersionInfo.Create(1, newVersionTag);

            // TargetDependenciy    
            Immutable.TargetDependency newTargetDependency = TargetDependency.Create("Ma target dépendence", "1.0.0");
            ImmutableList<TargetDependency> dependencies = ImmutableList.Create<TargetDependency>(newTargetDependency);

            // Targets
            Immutable.Target newTarget = Target.Create("Ma target", dependencies);
            ImmutableList<Target> targets = ImmutableList.Create<Target>(newTarget);

            // Projects
            Immutable.Project newProject = Project.Create("Chemin/vers/projet", targets);
            Immutable.BasicProject newBasicProject = BasicProject.Create(newProject);
            ImmutableList<BasicProject> projects = ImmutableList.Create<BasicProject>(newBasicProject);

            // BasicGitBranch
            Immutable.GitBranch newGitBranch = GitBranch.Create("Ma git branch", newBranchVersionInfo, projects);
            Immutable.BasicGitBranch newGitBasicBranch = Immutable.BasicGitBranch.Create(newGitBranch);
            ImmutableList<BasicGitBranch> listBasicGitBranch = ImmutableList.Create<BasicGitBranch>(newGitBasicBranch);

            // GitRepositories
            Immutable.GitRepository newGitRepository = GitRepository.Create("path", new Uri("http://uri"), listBasicGitBranch);
            Immutable.BasicGitRepository newBasicGitRepository = BasicGitRepository.Create(newGitRepository);
            ImmutableList<BasicGitRepository> repositories = ImmutableList.Create<BasicGitRepository>(newBasicGitRepository);

            // PackageInstances
            Immutable.PackageInstance newPackageInstance = PackageInstance.Create("Mon packageInstance", "Version ");
            ImmutableList<PackageInstance> instances = ImmutableList.Create<PackageInstance>(newPackageInstance);

            // PackageFeeds
            Immutable.PackageFeed newPackageFeed = PackageFeed.Create(new Uri("http://myPackageFeed"), instances);
            ImmutableList<PackageFeed> feeds = ImmutableList.Create<PackageFeed>(newPackageFeed);

            // Context
            return Immutable.GoMContext.Create("myContextPath", repositories, feeds);
        }

        [Fact]
        public void SetRepositoryDetails_method_returns_a_new_GoMContext_with_new_details()
        {
            var context = CreateTestGoMContext();
            Immutable.GitRepository newGitRepository = GitRepository.Create("newPath", new Uri("http://newUri"));
            context = context.SetRepositoryDetails(newGitRepository);

            var repo = context.Repositories.SingleOrDefault(rep => rep.Details == newGitRepository);
            repo.Should().NotBeNull();

            repo.Details.Should().Be(newGitRepository);
        }

        [Fact]
        public void UpdateRepositoryFields_method_returns_a_new_GoMContext()
        {
            var context = CreateTestGoMContext();
            var newContext = context.UpdateRepositoryFields("path", "newPath");

            newContext.Repositories[0].Path.Should().Be("newPath");
            newContext.Repositories[0].Url.Should().Be(context.Repositories[0].Url);
            newContext.Repositories[0].Details.Path.Should().Be("newPath");
            newContext.Repositories[0].Details.Url.Should().Be(context.Repositories[0].Details.Url);
            newContext.Repositories[0].Details.Branches.Should().BeEquivalentTo(context.Repositories[0].Details.Branches);
        }
    }
}

using System;
using Xunit;
using System.Collections.Immutable;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoM.Core.Immutable.Tests
{
    public class ImmutableCreationTests
    {

        private Immutable.GoMContext CreateTestGomContext()
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
        public void Check_testGomContext_creation_shouldNotBeNull()
        {
            var testGoM = CreateTestGomContext();
            testGoM.Should().NotBeNull();

        }

    }
}

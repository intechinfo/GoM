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

        private Immutable.GoMContext CreateTestGoMContext()
        {
            // BranchVersionInfo
            Immutable.VersionTag newVersionTag = VersionTag.Create("Version 1.0.0");
            Immutable.VersionTag newVersionTag2 = VersionTag.Create("Version 1.0.1");

            Immutable.BranchVersionInfo newBranchVersionInfo = BranchVersionInfo.Create(1, newVersionTag);
            Immutable.BranchVersionInfo newBranchVersionInfo2 = BranchVersionInfo.Create(2, newVersionTag2);

            // TargetDependenciy    
            Immutable.TargetDependency newTargetDependency = TargetDependency.Create("Ma target dépendence", "1.0.0");
            Immutable.TargetDependency newTargetDependency2 = TargetDependency.Create("Ma target dépendence 2", "1.0.1");
            ImmutableList<TargetDependency> dependencies = ImmutableList.Create<TargetDependency>(newTargetDependency, newTargetDependency2);

            // Targets
            Immutable.Target newTarget = Target.Create("Ma target", dependencies);
            Immutable.Target newTarget2 = Target.Create("Ma target2", dependencies);
            ImmutableList<Target> targets = ImmutableList.Create<Target>(newTarget, newTarget2);

            // Projects
            Immutable.Project newProject = Project.Create("Chemin/vers/projet", targets);
            Immutable.Project newProject2 = Project.Create("Chemin/vers/projet2", targets);
            Immutable.BasicProject newBasicProject = BasicProject.Create(newProject);
            Immutable.BasicProject newBasicProject2 = BasicProject.Create(newProject2);
            ImmutableList<BasicProject> projects = ImmutableList.Create<BasicProject>(newBasicProject, newBasicProject2);

            // BasicGitBranch
            Immutable.GitBranch newGitBranch = GitBranch.Create("Ma git branch", newBranchVersionInfo, projects);
            Immutable.GitBranch newGitBranch2 = GitBranch.Create("Ma git branch2", newBranchVersionInfo2, projects);

            Immutable.BasicGitBranch newGitBasicBranch = Immutable.BasicGitBranch.Create(newGitBranch);
            Immutable.BasicGitBranch newGitBasicBranch2 = Immutable.BasicGitBranch.Create(newGitBranch2);
            ImmutableList<BasicGitBranch> listBasicGitBranch = ImmutableList.Create<BasicGitBranch>(newGitBasicBranch, newGitBasicBranch2);

            // GitRepositories
            Immutable.GitRepository newGitRepository = GitRepository.Create("path", new Uri("http://uri"), listBasicGitBranch);
            Immutable.GitRepository newGitRepository2 = GitRepository.Create("path2", new Uri("http://uri2"), listBasicGitBranch);

            Immutable.BasicGitRepository newBasicGitRepository = BasicGitRepository.Create(newGitRepository);
            Immutable.BasicGitRepository newBasicGitRepository2 = BasicGitRepository.Create(newGitRepository2);

            ImmutableList<BasicGitRepository> repositories = ImmutableList.Create<BasicGitRepository>(newBasicGitRepository, newBasicGitRepository2);
            
            // PackageInstances
            Immutable.PackageInstance newPackageInstance = PackageInstance.Create("Mon packageInstance", "Version 1.0.0");
            Immutable.PackageInstance newPackageInstance2 = PackageInstance.Create("Mon packageInstance2", "Version 2.0.0");
            ImmutableList<PackageInstance> instances = ImmutableList.Create<PackageInstance>(newPackageInstance, newPackageInstance2);

            // PackageFeeds
            Immutable.PackageFeed newPackageFeed = PackageFeed.Create(new Uri("http://myPackageFeed"), instances);
            Immutable.PackageFeed newPackageFeed2 = PackageFeed.Create(new Uri("http://myPackageFeed2"), instances);
            ImmutableList<PackageFeed> feeds = ImmutableList.Create<PackageFeed>(newPackageFeed, newPackageFeed2);

            // Context
            return Immutable.GoMContext.Create("myContextPath", repositories, feeds);
        }

        [Fact]
        public void Check_testGomContext_creation_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            testGoM.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_creation_with_an_other_context_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);
            otherCtx.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_feeds_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Feeds.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_feeds_firstElement_packages_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Feeds[0].Packages.Should().NotBeNull();
            otherCtx.Feeds[0].Packages.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_feeds_firstElement_packages_firstElement_name_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Feeds[0].Packages[0].Name.Should().NotBeNull();
            otherCtx.Feeds[0].Packages[0].Name.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_feeds_firstElement_packages_firstElement_version_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Feeds[0].Packages[0].Version.Should().NotBeNull();
            otherCtx.Feeds[0].Packages[0].Version.Should().NotBeNull();
        }


        [Fact]
        public void Check_testGomContext_feeds_firstElement_url_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Feeds[0].Url.Should().NotBeNull();
            otherCtx.Feeds[0].Url.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories.Should().NotBeEmpty();
            otherCtx.Repositories.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_url_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Url.Should().NotBeNull();
            otherCtx.Repositories[0].Url.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_repositories_firstElement_path_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Path.Should().NotBeNull();
            otherCtx.Repositories[0].Path.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches.Should().NotBeEmpty();
            otherCtx.Repositories[0].Details.Branches.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_name_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Name.Should().NotBeEmpty();
            otherCtx.Repositories[0].Details.Branches[0].Name.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_lastTagDepth_shouldBeGreterThan0()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.LastTagDepth.Should().BeGreaterThan(0);
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.LastTagDepth.Should().BeGreaterThan(0);
        }
        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_lastTag_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.LastTag.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.LastTag.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_lastTag_fullName_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.LastTag.FullName.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.LastTag.FullName.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_path_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Path.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Path.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_name_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Name.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Name.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_firstElement_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);
            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_firstElement_name_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Name.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Name.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_firstElement_version_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Version.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Version.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_rootPath_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = GoMContext.Create(testGoM);

            testGoM.RootPath.Should().NotBeEmpty();
            otherCtx.RootPath.Should().NotBeEmpty();
        }
    }
}

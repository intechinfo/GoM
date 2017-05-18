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

            // FIXME : Erreur dans les tests ligne 258. Les détails des projets seraient nuls

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

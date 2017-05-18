using System;
using Xunit;
using FluentAssertions;

namespace GoM.Core.Mutable.Tests
{
    public class MutableCreationTests
    {
        public GoMContext CreateTestGoMContext()
        {
            #region context
            // GoMContext
            GoMContext newGoMContext = new GoMContext();
            newGoMContext.RootPath = "my/root/path";

            #region basicGitRepository
            // BasicGitRepository
            var newBasicGitRepository = new BasicGitRepository();
            newBasicGitRepository.Path = "my/basicGitRepo/path";
            newBasicGitRepository.Url = new Uri("http://my/basicGitRepo/Url");

            #region gitRepository
            // GitRepository
            var newGitRepository = new GitRepository();
            newGitRepository.Path = "my/branch/path";
            newGitRepository.Url = new Uri("http://my/branch/uri");

            #region basicGitBranch
            // BasicGitBranch
            var newBasicGitBranch = new BasicGitBranch();
            newBasicGitBranch.Name = "myBasicGitBranch";

            #region gitBranch
            // GitBranch
            var newGitBranch = new GitBranch();
            newGitBranch.Name = "myGitBranch";
            #region branchVersionInfo
            // BranchVersionInfo
            var newBranchVersionInfo = new BranchVersionInfo();
            newBranchVersionInfo.LastTagDepth = 1;

            #region versionTag
            // VersionTag
            var newVersionTag = new VersionTag();
            newVersionTag.FullName = "version tag v1.0.0";
            #endregion

            newBranchVersionInfo.LastTag = newVersionTag;
            #endregion

            newGitBranch.Version = newBranchVersionInfo;
            #endregion

            // Project
            var newProject = new Project();
            newProject.Path = "my/project/path";

            // BasicProject
            var newBasicProject = new BasicProject();
            newBasicProject.Path = "my/basicProject/path";
            newBasicProject.Details = newProject;

            #region target
            // Target
            var newTarget = new Target();
            newTarget.Name = "myTarget x64";

            newProject.Targets.Add(newTarget);
            #region targetDependency
            // TargetDependency
            var newDependency = new TargetDependency();
            newDependency.Name = "myDependency";
            newDependency.Version = "v1.0.0";
            #endregion

            newTarget.Dependencies.Add(newDependency);
            #endregion
            newGitBranch.Projects.Add(newBasicProject);

            newBasicGitBranch.Details = newGitBranch;
            #endregion
            newGitRepository.Branches.Add(newBasicGitBranch);
            # endregion

            newBasicGitRepository.Details = newGitRepository;
            # endregion

            newGoMContext.Repositories.Add(newBasicGitRepository);

            #region packageFeed
            // PackageFeed
            var newPackageFeed = new PackageFeed();
            newPackageFeed.Url = new Uri("http://myurl");

            #region packageInstance
            // PackageInstance
            var newPackageInstance = new PackageInstance();
            newPackageInstance.Name = "myPackage";
            newPackageInstance.Version = "v1.0.0";
            #endregion

            newPackageFeed.Packages.Add(newPackageInstance);
            #endregion

            newGoMContext.Feeds.Add(newPackageFeed);
            # endregion

            return newGoMContext;
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
            var otherCtx = new GoMContext(testGoM);
            otherCtx.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_feeds_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Feeds.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_feeds_firstElement_packages_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Feeds[0].Packages.Should().NotBeNull();
            otherCtx.Feeds[0].Packages.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_feeds_firstElement_packages_firstElement_name_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Feeds[0].Packages[0].Name.Should().NotBeNull();
            otherCtx.Feeds[0].Packages[0].Name.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_feeds_firstElement_packages_firstElement_version_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Feeds[0].Packages[0].Version.Should().NotBeNull();
            otherCtx.Feeds[0].Packages[0].Version.Should().NotBeNull();
        }


        [Fact]
        public void Check_testGomContext_feeds_firstElement_url_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Feeds[0].Url.Should().NotBeNull();
            otherCtx.Feeds[0].Url.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories.Should().NotBeEmpty();
            otherCtx.Repositories.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_url_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Url.Should().NotBeNull();
            otherCtx.Repositories[0].Url.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_repositories_firstElement_path_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Path.Should().NotBeNull();
            otherCtx.Repositories[0].Path.Should().NotBeNull();
        }
        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches.Should().NotBeEmpty();
            otherCtx.Repositories[0].Details.Branches.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_name_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Name.Should().NotBeEmpty();
            otherCtx.Repositories[0].Details.Branches[0].Name.Should().NotBeEmpty();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_lastTagDepth_shouldBeGreterThan0()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.LastTagDepth.Should().BeGreaterThan(0);
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.LastTagDepth.Should().BeGreaterThan(0);
        }
        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_lastTag_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.LastTag.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.LastTag.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_version_lastTag_fullName_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Version.LastTag.FullName.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Version.LastTag.FullName.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_path_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Path.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Path.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_name_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Name.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Name.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_firstElement_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);
            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_firstElement_name_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Name.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Name.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_repositories_firstElement_details_branches_firstElement_details_projects_firstElement_targets_firstElement_dependencies_firstElement_version_shouldNotBeNull()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Version.Should().NotBeNull();
            otherCtx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets[0].Dependencies[0].Version.Should().NotBeNull();
        }

        [Fact]
        public void Check_testGomContext_rootPath_shouldNotBeEmpty()
        {
            var testGoM = CreateTestGoMContext();
            var otherCtx = new GoMContext(testGoM);

            testGoM.RootPath.Should().NotBeEmpty();
            otherCtx.RootPath.Should().NotBeEmpty();
        }

    }
}

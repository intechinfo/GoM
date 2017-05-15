using System;
using Xunit;
using FluentAssertions;

namespace GoM.Core.Mutable.Tests
{
    public class MutableCreationTests
    {
        private GoMContext CreateTestGoMContext()
        {
            #region context
            // GoMContext
            var newGoMContext = new GoMContext();
            newGoMContext.RootPath = "my/root/path";

            #region basicGitRepository
            // BasicGitRepository
            var newBasicGitRepository = new BasicGitRepository();
            newBasicGitRepository.Path = "my/basicGitRepo/path";
            newBasicGitRepository.Url = new Uri("my/basicGitRepo/Url");

            #region gitRepository
            // GitRepository
            var newGitRepository = new GitRepository();
            newGitRepository.Path = "my/branch/path";
            newGitRepository.Url = new Uri("my/branch/uri");
            newGitRepository.Details = null;

            #region basicGitBranch
            // BasicGitBranch
            var newBasicGitBranch = new BasicGitBranch();
            newBasicGitBranch.Name = "myBasicGitBranch";

            #region gitBranch
            // GitBranch
            var newGitBranch = new GitBranch();
            newGitBranch.Name = "myGitBranch";
            newGitBranch.Details = null;
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
            #region target
            // Target
            var newTarget = new Target();
            newTarget.Name = "myTarget x64";
            #region targetDependency
            // TargetDependency
            var newDependency = new TargetDependency();
            newDependency.Name = "myDependency";
            newDependency.Version = "v1.0.0";
            #endregion

            newTarget.Dependencies.Add(newDependency);
            #endregion
            newGitBranch.Projects.Add(newProject);

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
            newPackageFeed.Url = new Uri("my/feed/url");

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

        public void Create_ALL_THE_THINGS_mutable()
        {
            #region context
            // GoMContext
            var newGoMContext = new GoMContext();
            newGoMContext.RootPath = "my/root/path";

            #region basicGitRepository
            // BasicGitRepository
            var newBasicGitRepository = new BasicGitRepository();
            newBasicGitRepository.Path = "my/basicGitRepo/path";
            newBasicGitRepository.Url = new Uri("my/basicGitRepo/Url");

            #region gitRepository
            // GitRepository
            var newGitRepository = new GitRepository();
            newGitRepository.Path = "my/branch/path";
            newGitRepository.Url = new Uri("my/branch/uri");
            newGitRepository.Details = null;

            #region basicGitBranch
            // BasicGitBranch
            var newBasicGitBranch = new BasicGitBranch();
            newBasicGitBranch.Name = "myBasicGitBranch";

            #region gitBranch
            // GitBranch
            var newGitBranch = new GitBranch();
            newGitBranch.Name = "myGitBranch";
            newGitBranch.Details = null;
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
            #region target
            // Target
            var newTarget = new Target();
            newTarget.Name = "myTarget x64";
            #region targetDependency
            // TargetDependency
            var newDependency = new TargetDependency();
            newDependency.Name = "myDependency";
            newDependency.Version = "v1.0.0";
            #endregion

            newTarget.Dependencies.Add(newDependency);
            #endregion
            newGitBranch.Projects.Add(newProject);

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
            newPackageFeed.Url = new Uri("my/feed/url");

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
        }

        public void Create_new_mutable_GoMContext_from_existing_IGoMContext()
        {

        }
    }
}

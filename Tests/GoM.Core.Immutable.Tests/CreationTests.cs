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
            


            Immutable.VersionTag newVersionTag = VersionTag.Create("Version 1.0.0");
            Immutable.BranchVersionInfo newBranchVersionInfo = BranchVersionInfo.Create(1, newVersionTag);
            Immutable.GitBranch newGitBranch = GitBranch.Create("Ma git branch", newBranchVersionInfo);
            

            Immutable.BasicGitBranch newGitBasicBranch = Immutable.BasicGitBranch.Create("Ma basic git branch", newGitBranch);

            Immutable.BasicGitRepository newBasicGitRepository = BasicGitRepository.Create("/my/path", new Uri("http://myBasicGitRepository"));
            ImmutableList<BasicGitRepository> repositories = ImmutableList.Create<BasicGitRepository>(newBasicGitRepository);


            Immutable.PackageInstance newPackageInstance = PackageInstance.Create("Min packageInstance", "Version ");

            ImmutableList<PackageInstance> instances = ImmutableList.Create<PackageInstance>(newPackageInstance);

            Immutable.PackageFeed newPackageFeed = PackageFeed.Create(new Uri("http://myPackageFeed"), instances);

            ImmutableList<PackageFeed> feeds = ImmutableList.Create<PackageFeed>(packageFeed);
            Immutable.GoMContext immutableGomContext = Immutable.GoMContext.Create(path, repositories, feeds);

            Immutable.GitRepository.Create(path, url, branches);
        }



        [Obsolete]
        private GitRepository CreateNewGitRepository()
        {
            string path = "my/root/path";
            Uri url = new Uri("http://myGitRepository");
            ImmutableList<BasicGitBranch> branches = ImmutableList.Create<BasicGitBranch>();
            return Immutable.GitRepository.Create(path, url, branches);
        }
    }
}

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
        ImmutableCreationTests _tests = new ImmutableCreationTests();

        [Fact]
        public void SetRepositoryDetails_method_returns_a_new_GoMContext_with_new_details()
        {
            var context = _tests.CreateTestGoMContext();
            Immutable.GitRepository newGitRepository = GitRepository.Create("newPath", new Uri("http://newUri"));
            var newContext = context.SetRepositoryDetails(context.Repositories[0], newGitRepository);

            var repo = newContext.Repositories.SingleOrDefault(rep => rep.Details == newGitRepository);
            repo.Should().NotBeNull();

            repo.Details.Should().Be(newGitRepository);
        }

        [Fact]
        public void SetBranchDetails_method_returns_a_new_GoMContext_with_new_details()
        {
            var context = _tests.CreateTestGoMContext();
            Immutable.GitBranch newGitBranch = GitBranch.Create("develop");
            var newCcontext = context.SetBranchDetails(context.Repositories[0].Details.Branches[0], newGitBranch);

            var repo = newCcontext.Repositories[0].Details.Branches.SingleOrDefault(rep => rep.Details == newGitBranch);
            repo.Should().NotBeNull();

            repo.Details.Should().Be(newGitBranch);
        }

        [Fact]
        public void UpdateRepositoryFields_method_returns_a_new_GoMContext()
        {
            var context = _tests.CreateTestGoMContext();
            var newContext = context.UpdateRepositoryFields(context.Repositories[0], "newPath");

            newContext.Repositories[0].Path.Should().Be("newPath");
            newContext.Repositories[0].Url.Should().Be(context.Repositories[0].Url);
            newContext.Repositories[0].Details.Path.Should().Be("newPath");
            newContext.Repositories[0].Details.Url.Should().Be(context.Repositories[0].Details.Url);
            newContext.Repositories[0].Details.Branches.Should().BeEquivalentTo(context.Repositories[0].Details.Branches);
        }

        [Fact]
        public void UpdateBranchName_method_returns_a_new_GoMContext()
        {
            var context = _tests.CreateTestGoMContext();
            var newContext = context.UpdateBranchName(context.Repositories[0].Details, "Ma git branch", "develop");

            newContext.Repositories[0].Details.Branches[0].Name.Should().Be("develop");
        }

        [Fact]
        public void AddFeeds()
        {
            var context = _tests.CreateTestGoMContext();
            PackageFeed pf = PackageFeed.Create(new Uri("http://maNouvelleUrl"), ImmutableList.Create<PackageInstance>());
            context = context.AddOrUpdatePackageFeeds(pf);
            context.Feeds[2].Should().Be(pf);
        }

        [Fact]
        public void UpdateFeeds()
        {
            var context = _tests.CreateTestGoMContext();
            PackageFeed pf = PackageFeed.Create(new Uri("http://myPackageFeed"), ImmutableList.Create<PackageInstance>());
            context = context.AddOrUpdatePackageFeeds(pf);
            context.Feeds[0].Should().Be(pf);

            Action act = () => context.Feeds[2].Should().NotBeNull();
            act.ShouldThrow<ArgumentOutOfRangeException>();


        }

    }
}

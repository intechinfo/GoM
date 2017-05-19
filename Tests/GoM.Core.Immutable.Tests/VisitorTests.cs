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
        ImmutableCreationTests gom = new ImmutableCreationTests();

        [Fact]
        public void SetRepositoryDetails_method_returns_a_new_GoMContext_with_new_details()
        {
            var context = gom.CreateTestGoMContext();
            Immutable.GitRepository newGitRepository = GitRepository.Create("newPath", new Uri("http://newUri"));
            context = context.SetRepositoryDetails(newGitRepository);

            var repo = context.Repositories.SingleOrDefault(rep => rep.Details == newGitRepository);
            repo.Should().NotBeNull();

            repo.Details.Should().Be(newGitRepository);
        }

        [Fact]
        public void UpdateRepositoryFields_method_returns_a_new_GoMContext()
        {
            var context = gom.CreateTestGoMContext();
            var newContext = context.UpdateRepositoryFields("path", "newPath");

            newContext.Repositories[0].Path.Should().Be("newPath");
        }

        [Fact]
        public void AddFeeds()
        {
            var context = gom.CreateTestGoMContext();
            PackageFeed pf = PackageFeed.Create(new Uri("http://maNouvelleUrl"), ImmutableList.Create<PackageInstance>());
            context = context.AddOrUpdatePackageFeeds(pf);
            context.Feeds[2].Should().Be(pf);
        }

        [Fact]
        public void UpdateFeeds()
        {
            var context = gom.CreateTestGoMContext();
            PackageFeed pf = PackageFeed.Create(new Uri("http://myPackageFeed"), ImmutableList.Create<PackageInstance>());
            context = context.AddOrUpdatePackageFeeds(pf);
            context.Feeds[0].Should().Be(pf);

            Action act = () => context.Feeds[2].Should().NotBeNull();
            act.ShouldThrow<ArgumentOutOfRangeException>();


        }

    }
}

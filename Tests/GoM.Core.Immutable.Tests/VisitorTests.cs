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
            var newContext = context.AddOrSetRepositoryDetails(newGitRepository);

            var repo = newContext.Repositories.SingleOrDefault(rep => rep.Details == newGitRepository);
            repo.Should().NotBeNull();

            repo.Details.Should().Be(newGitRepository);
        }

        [Fact]
        public void SetBranchDetails_method_returns_a_new_GoMContext_with_new_details()
        {
            var context = _tests.CreateTestGoMContext();
            Immutable.GitBranch newGitBranch = GitBranch.Create("develop");
            var newCcontext = context.AddOrSetBranchDetails(context.Repositories[0].Details.Branches[0], newGitBranch);

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
    }
}

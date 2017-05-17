using System;
using Xunit;
using System.Collections.Immutable;
using FluentAssertions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GoM.Core.Immutable.Tests
{
    public class CreationTests
    {
        //[Fact]
        //public void adding_updating_and_removing_repositories()
        //{
        //    // Create BasicGitRepository immutable list
        //    var repo = BasicGitRepository.Create("path", new Uri("http://gitBasicUrl"));
        //    var repositories = ImmutableList.Create(repo);

        //    // Create PackageFeed immutalbe list
        //    var packages = ImmutableList.Create(PackageFeed.Create());

        //    // Create gomContext
        //    var gomContext = GoMContext.Create("my/path", repositories, packages);
        //    gomContext.Repositories[0].Path.Should().Be("path");

        //    // Add a new repository to the list
        //    gomContext = gomContext.AddRepository(BasicGitRepository.Create("otherpath", new Uri("http://otherGitBasicUrl")));
        //    gomContext.Repositories.Count.Should().Be(2);
        //    gomContext.Repositories[1].Path.Should().Be("otherpath");

        //    // Update an existing repository
        //    gomContext = gomContext.UpdateRepository(gomContext.Repositories[0], "newPath");
        //    gomContext.Repositories[0].Path.Should().Be("newPath");
        //}

        //[Fact]
        //public void test_toUpper()
        //{
        //    // Create BasicGitRepository immutable list
        //    var repo = BasicGitRepository.Create("upperCase", new Uri("http://gitBasicUrl"));
        //    var repo2 = BasicGitRepository.Create("lowercasePath", new Uri("http://gitBasicUrl"));
        //    var repo3 = BasicGitRepository.Create("allcapspath", new Uri("http://gitBasicUrl"));
        //    var repositories = ImmutableList.Create(repo, repo2, repo3);

        //    // Create PackageFeed immutalbe list
        //    var packages = ImmutableList.Create(PackageFeed.Create());

        //    // Create gomContext
        //    var gomContext = GoMContext.Create("my/path/upper", repositories, packages);

        //    var vistor = new GoMContext.ToUppercaseVisitor("upper");
        //    GoMContext newContext = vistor.Visit(gomContext);

        //    newContext.Should().Be(gomContext.RootPath.ToUpperInvariant());
        //}

    }
}

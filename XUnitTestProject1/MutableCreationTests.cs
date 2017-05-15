using System;
using Xunit;
using FluentAssertions;

namespace GoM.Core.Mutable.Tests
{
    public class MutableCreationTests
    {
        [Fact]
        public void Project_constructor_return_an_empty_Project_instance()
        {
            var newProject = new Project("my/path");
            newProject.Path.Should().Be("my/path");
            newProject.Targets.Should().BeEmpty();
        }

        [Fact]
        public void Target_constructor_return_an_empty_Target_instance()
        {
            var newTarget = new Target("myTestTarget x64");
            newTarget.Name.Should().Be("myTestTarget x64");
            newTarget.Dependencies.Should().BeEmpty();

            var newDependency = new TargetDependency("myDependency", "v1.0.0");
            newTarget.Dependencies.Add(newDependency);

            newTarget.Dependencies[0].Should().Be(newDependency);
        }
    }
}

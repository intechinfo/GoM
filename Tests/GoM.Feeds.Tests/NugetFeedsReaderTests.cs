using FluentAssertions;
using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace GoM.Feeds.Tests
{
    public class NugetFeedsReaderTests
    {
        private NugetOrgFeedReader CreateReader() => new NugetOrgFeedReader();
        [Fact]
        public void Check_Reader_creation_shouldNotBeNull()
        {
            var testReader = CreateReader();
            testReader.Should().NotBeNull();
        }
        [Fact]
        public void Check_Reader_FeedMatch_works()
        {
            var testReader = CreateReader();
            testReader.FeedMatch(new Uri("http://api.nuget.org/v3/index.json")).Result.Should().Be(true);
             testReader.FeedMatch(new Uri("http://google.com/")).Result.Should().Be(false);
        }

        [Fact]
        public void Check_Reader_Get_Newest_Versions()
        {
            var testReader = CreateReader();

            testReader.GetNewestVersions("NUnit", "3.4.0").Result.Should().NotBeNullOrEmpty();
            testReader.GetNewestVersions("NUnit", "3.6.1").Result.Should().BeNullOrEmpty();
            Action a2 = () => { IEnumerable<IPackageInstance> b = testReader.GetNewestVersions("NUnit", "blabla").Result; };
            a2.ShouldThrow<ArgumentException>();

            Action a1 = () => { IEnumerable<IPackageInstance> b = testReader.GetNewestVersions("", "3.6.1").Result; };
            a1.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void Check_Reader_Get_All_Versions()
        {
            var testReader = CreateReader();

            testReader.GetAllVersions("NUnit").Result.Should().NotBeNullOrEmpty();
            //testReader.GetAllVersions("PackageMustn0TExISte").Result.Should().BeNullOrEmpty();

            Action a1 = () => { IEnumerable<Core.IPackageInstance> b = testReader.GetAllVersions("PackageMustn0TExISte").Result; };
            a1.ShouldThrow<ArgumentException>();

            Action a2 = () => { IEnumerable<Core.IPackageInstance> b = testReader.GetAllVersions("").Result; };
            a2.ShouldThrow<ArgumentException>();
            
        }
    }
}

using FluentAssertions;
using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using GoM.Feeds.Results;
using System.Net;

namespace GoM.Feeds.Tests
{
    public class PypiFeedsReaderTests
    {
        private PypiOrgFeedReader CreateReader() => new PypiOrgFeedReader();
        [Fact]
        public void Check_Reader_creation_shouldNotBeNull()
        {
            using (var testReader = CreateReader())
            {
                testReader.Should().NotBeNull();
            }
        }
        [Fact]
        public void Check_Reader_FeedMatch_works()
        {
            using (var testReader = CreateReader())
            {
                testReader.FeedMatch(new Uri("https://pypi.python.org/pypi/Python/json")).Result.Result.Should().Be(true);
                testReader.FeedMatch(new Uri("http://google.com/")).Result.Result.Should().Be(false);
            }
        }
        [Fact]
        public void Check_Reader_Get_Newest_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetNewestVersions("colorama", "0.1.5").Result.Result.Should().NotBeNullOrEmpty();
                testReader.GetNewestVersions("colorama", "0.3.9").Result.Result.Count(x=>x.Success).Should().Be(0);

                Action a1 = () => { var b = testReader.GetNewestVersions("colorama", "blabla").Result; };
                a1.ShouldThrow<ArgumentException>();

                Action a2 = () => { var b = testReader.GetNewestVersions("", "3.6.1").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }
        [Fact]
        public void Check_Reader_Get_All_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetAllVersions("colorama").Result.Result.Should().NotBeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Result.Should().BeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Json.StatusCode.Should().Be(HttpStatusCode.NotFound);

                Action a2 = () => {var b = testReader.GetAllVersions("").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }
        [Fact]
        public void Check_Reader_Get_Dependencies()
        {
            var testReader = CreateReader();

            testReader.GetDependencies("pyramid", "1.8.3").Result.Result.Should().NotBeNullOrEmpty();
            
            testReader.GetDependencies("colorama", "0.1.5").Result.Result.Count(x=>x.Result.Dependencies.Count()!=0).Should().Be(0);

            Action a1 = () => { var b = testReader.GetDependencies("", "3.4.0").Result; };
            a1.ShouldThrow<ArgumentException>();

            Action a2 = () => { var b = testReader.GetDependencies("pyramid", "").Result; };
            a1.ShouldThrow<ArgumentException>();
        }
    }
}

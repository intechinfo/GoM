using FluentAssertions;
using System;
using System.Linq;
using System.Net;
using Xunit;


namespace GoM.Feeds.Tests
{
    public class NugetFeedsReaderTests
    {
        private NugetOrgFeedReader CreateReader() => new NugetOrgFeedReader();
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
                testReader.FeedMatch(new Uri("http://api.nuget.org/v3/index.json")).Result.Result.Should().Be(true);
                testReader.FeedMatch(new Uri("http://google.com/")).Result.Result.Should().Be(false);
            }
        }

        [Fact]
        public void Check_Reader_Get_Newest_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetNewestVersions("NUnit", "3.4.0").Result.Result.Should().NotBeNullOrEmpty();

                testReader.GetNewestVersions("NUnit", "3.7.0").Result.Result.Where(x => x.Success).Count().Should().Be(0);

                testReader.GetNewestVersions("NUnit", "4.6.1").Result.Result.Where(x => x.Success).Count().Should().Be(0);

                Action a1 = () => { var b = testReader.GetNewestVersions("", "3.6.1").Result; };
                a1.ShouldThrow<ArgumentException>();

                Action a2 = () => { var b = testReader.GetNewestVersions("NUnit", "blabla").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void Check_Reader_Get_All_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetAllVersions("NUnit").Result.Result.Should().NotBeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Result.Should().BeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Json.StatusCode.Should().Be(HttpStatusCode.NotFound);

                Action a2 = () => { var b = testReader.GetAllVersions("").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void Check_Reader_Get_Dependencies()
        {
            var testReader = CreateReader();

            testReader.GetDependencies("NUnit", "3.6.1").Result.Result.Should().NotBeNullOrEmpty();
            testReader.GetDependencies("CK.Core", "7.0.0").Result.Result.Should().NotBeNullOrEmpty();

            testReader.GetDependencies("NUnit", "3.4.0").Result.Result.Count(x => x.Result.Dependencies.Count() != 0).Should().Be(0);

            Action a1 = () => { var b = testReader.GetDependencies("", "3.4.0").Result; };
            a1.ShouldThrow<ArgumentException>();

            Action a2 = () => { var b = testReader.GetDependencies("NUnit", "").Result; };
            a1.ShouldThrow<ArgumentException>();          
        }
    }
}

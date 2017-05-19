using FluentAssertions;
using System;
using System.Linq;
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

                testReader.GetNewestVersions("NUnit", "3.6.1").Result.Result.Where(x => x.Success).Count().Should().Be(0);

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
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Json.JsonException.Should().NotBeNull();

                Action a2 = () => { var b = testReader.GetAllVersions("").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }
    }
}

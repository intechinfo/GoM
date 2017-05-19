using FluentAssertions;
using System;
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
                testReader.GetNewestVersions("NUnit", "3.6.1").Result.Result.Should().BeNullOrEmpty();
                testReader.GetNewestVersions("NUnit", "blabla").Result.Error.Should().BeOfType(typeof(ArgumentException));
                Action a1 = () => { var b = testReader.GetNewestVersions("", "3.6.1"); };
                a1.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void Check_Reader_Get_All_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetAllVersions("NUnit").Result.Result.Should().NotBeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Result.Should().BeNullOrEmpty();

                testReader.GetAllVersions("PackageMustn0TExISte").Result.Error.Should().BeOfType(typeof(ArgumentException));

                Action a2 = () => { var b = testReader.GetAllVersions(""); };
                a2.ShouldThrow<ArgumentException>();
            }
        }
    }
}

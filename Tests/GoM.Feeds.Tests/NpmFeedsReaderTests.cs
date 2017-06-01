using FluentAssertions;
using Semver;
using System;
using System.Linq;
using Xunit;

namespace GoM.Feeds.Tests
{
    public class NpmFeedsReaderTests
    {
        private NpmJsFeedReader CreateReader() => new NpmJsFeedReader();
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
                testReader.FeedMatch(new Uri("http://registry.npmjs.org/")).Result.Result.Should().Be(true);
                testReader.FeedMatch(new Uri("http://google.com/")).Result.Result.Should().Be(false);
            }
        }
        [Fact]
        public void Check_Reader_Get_Newest_Versions()
        {
            using (var testReader = CreateReader())
            {
              
                var ReturnedPackages = testReader.GetNewestVersions("sugar", "0.0.0").Result.Result;

                SemVersion latestVersion = ReturnedPackages.Where(x => x.Success == true && x.Result != null).Max(x => x.Result.Version);

                var unexistingVersion = new SemVersion(latestVersion.Major + 1, latestVersion.Minor, latestVersion.Patch);

                testReader.GetNewestVersions("sugar", "1.1.0").Result.Result.Should().NotBeNullOrEmpty();

                testReader.GetNewestVersions("sugar", latestVersion.ToString()).Result.Result.Where(x => x.Success).Count().Should().Be(0);

                testReader.GetNewestVersions("sugar", unexistingVersion.ToString()).Result.Result.Where(x => x.Success).Count().Should().Be(0);


                Action a1 = () => { var b = testReader.GetNewestVersions("", "3.6.1").Result; };
                a1.ShouldThrow<ArgumentException>();
                Action a2 = () => { var b = testReader.GetNewestVersions("blabla", "blabla").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void Check_Reader_Get_All_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetAllVersions("sugar").Result.Result.Should().NotBeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Result.Should().BeNullOrEmpty();

                Action a2 = () => { var b = testReader.GetAllVersions("").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }
    }
}

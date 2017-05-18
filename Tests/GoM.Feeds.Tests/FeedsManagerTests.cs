using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using GoM.Core.Mutable;
using GoM.Core;
using System.Linq;
namespace GoM.Feeds.Tests
{
    public class FeedsManagerTests
    {
        private FeedManager GetManager()
        {
            return new FeedManager();
        }
        private List<Uri> GetUriList()
        {
            return new List<Uri>
            {
                new Uri("http://www.google.com"),
                new Uri("http://registry.npmjs.org/"),
                new Uri("https://api.nuget.org/v3/index.json"),
                new Uri("http://api.nuget.org/v3/index.json"),
                new Uri("http://api.nuget.org/v3/index.json"),
                new Uri("https://pypi.python.org/pypi/Python/json")
            };
        }
        private PackageInstance GetCrossPackage()
        {
            return new PackageInstance { Name = "python", Version = "0" };
        }
        [Fact]
        public void check_manager_creation_shouldNotBeNull()
        {
            using (var testManager = GetManager())
            {
                testManager.Should().NotBeNull();
            }
        }
        [Fact]
        public void GetAllVersions_should_return_data_from_each_feed_available()
        {
            using (var testManager = GetManager())
            {
                var pkg = GetCrossPackage();
                var pkgList = new List<IPackageInstance> { pkg };
                var res = testManager.GetAllVersions(GetUriList(), pkgList);
                res.Keys.Count.Should().Be(1);
                res.Keys.Single().Should().Be(pkg);
                res[pkg].Should().NotBeNullOrEmpty();
            }
        }
        [Fact]
        public void GetNewestVersions_should_return_data_from_each_feed_available()
        {
            using (var testManager = GetManager())
            {
                var pkg = GetCrossPackage();
                var pkgList = new List<IPackageInstance> { pkg };
                var res = testManager.GetNewestVersions(GetUriList(), pkgList);
                res.Keys.Count.Should().Be(1);
                res.Keys.Single().Should().Be(pkg);
                res[pkg].Should().NotBeNullOrEmpty();
            }
        }
    }
}

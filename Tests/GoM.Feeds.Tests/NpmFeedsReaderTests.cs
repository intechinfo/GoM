﻿using FluentAssertions;
using GoM.Core;
using System;
using System.Collections.Generic;
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
                testReader.FeedMatch(new Uri("http://registry.npmjs.org/")).Result.Should().Be(true);
                testReader.FeedMatch(new Uri("http://google.com/")).Result.Should().Be(false);
            }
        }
        [Fact]
        public void Check_Reader_Get_Newest_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetNewestVersions("sugar", "1.1.0").Result.Should().NotBeNullOrEmpty();
                testReader.GetNewestVersions("sugar", "2.0.4").Result.Should().BeNullOrEmpty();
                Action a2 = () => { IEnumerable<IPackageInstance> b = testReader.GetNewestVersions("sugar", "blabla").Result; };
                a2.ShouldThrow<ArgumentException>();

                Action a1 = () => { IEnumerable<IPackageInstance> b = testReader.GetNewestVersions("", "3.6.1").Result; };
                a1.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void Check_Reader_Get_All_Versions()
        {
            using (var testReader = CreateReader())
            {
                testReader.GetAllVersions("sugar").Result.Should().NotBeNullOrEmpty();
                testReader.GetAllVersions("PackageMustn0TExISte").Result.Should().BeNullOrEmpty();

                Action a2 = () => { IEnumerable<Core.IPackageInstance> b = testReader.GetAllVersions("").Result; };
                a2.ShouldThrow<ArgumentException>();
            }
        }
    }
}
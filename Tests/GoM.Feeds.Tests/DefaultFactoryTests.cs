using FluentAssertions;
using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GoM.Feeds.Tests
{
    public class DefaultFactoryTests
    {
        private DefaultFeedFactory GetFactory()
        {
            return new DefaultFeedFactory();
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
        [Fact]
        public void Check_reader_creation_shouldNotBeNull()
        {
            var testFactory = GetFactory();
            testFactory.Should().NotBeNull();
            testFactory.FeedReaders.Should().NotBeNull();
            testFactory.FeedReaders.Count().Should().Be(3);
        }
        [Fact]
        public void Snif_returns_values_from_all_feeds()
        {
            var testFactory = GetFactory();
            IEnumerable<IFeedReader> result = testFactory.Snif(new List<Uri>());
            result.Should().NotBeNull();
            result.Count().Should().Be(0);

            testFactory = GetFactory();
            result = testFactory.Snif(GetUriList());
            result.Should().NotBeNull();
            result.Count().Should().NotBe(0);
        }
    }
}

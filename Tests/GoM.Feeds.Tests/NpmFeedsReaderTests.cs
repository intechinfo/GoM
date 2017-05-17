using FluentAssertions;
using System;
using Xunit;

namespace GoM.Feeds.Tests
{
    public class NpmFeedsReaderTests
    {
        private NpmJsFeedReader CreateReader() => new NpmJsFeedReader();
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
            testReader.FeedMatch(new Uri("http://registry.npmjs.org/")).Result.Should().Be(true);
            //Action a1 = () => { bool b = testReader.FeedMatch(new Uri("aaaa")).Result; };
            //Action a2 = () => { bool b = testReader.FeedMatch(new Uri("")).Result; };
            testReader.FeedMatch(new Uri("http://google.com/")).Result.Should().Be(false);
        }
    }
}

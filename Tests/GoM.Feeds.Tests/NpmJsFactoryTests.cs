using System;
using System.Collections.Generic;
using System.Text;
using GoM.Feeds;
using GoM.Feeds.Abstractions;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace GoM.Feeds.Tests
{
    public class NpmJsFactoryTests
    {
        [Fact]
        public void create_factory_works()
        {
            using (NpmJsFactory sut = new NpmJsFactory())
            {
                sut.Should().NotBeNull();
            }
        }

        [Fact]
        public void sniff_js_with_proper_single_uri()
        {
            using (NpmJsFactory fac = new NpmJsFactory())
            {
                Uri myUri = new Uri("http://registry.npmjs.org/");
                IEnumerable<IFeedReader> res = fac.Snif(myUri);
                var sut = res.ToList();
                sut.Count.Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void sniff_js_with_bad_single_uri_must_return_empty()
        {
            using (NpmJsFactory fac = new NpmJsFactory())
            {
                Uri myUri = new Uri("http://www.google.com");
                IEnumerable<IFeedReader> res = fac.Snif(myUri);
                var sut = res.ToList();
                sut.Count().Should().Be(0);
            }
        }
        [Fact]
        public void sniff_js_with_list_of_uris() 
        {
            Uri myUri = new Uri("http://registry.npmjs.org/");
            List<Uri> myList = new List<Uri>();
            myList.Add(myUri);
            using (NpmJsFactory fac = new NpmJsFactory())
            {
                IEnumerable<IFeedReader> res = fac.Snif(myList);
                var sut = res.ToList();
                sut.Count().Should().BeGreaterThan(0);
            }
        }

    }
}

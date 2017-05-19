using System;
using System.Collections.Generic;
using System.Text;
using GoM.Feeds;
using GoM.Feeds.Results;
using GoM.Feeds.Abstractions;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace GoM.Feeds.Tests
{
    public class PypiFactoryTests
    {
        [Fact]
        public void create_factory_works()
        {
            using (PypiFactory sut = new PypiFactory()) 
            {
                sut.Should().NotBeNull();
            }
            
        }

        [Fact]
        public void sniff_python_with_proper_single_uri()
        {
            using (PypiFactory fac = new PypiFactory()) 
            {
                Uri myUri = new Uri("https://pypi.python.org/pypi/Python/json");
                var res = fac.Snif(myUri);
                var sut = res.Result.ToList();
                sut.Count().Should().BeGreaterThan(0);
            }
        }

        [Fact]
        public void sniff_python_with_bad_single_uri_must_return_empty()
        {
            using (PypiFactory fac = new PypiFactory())
            {
                Uri myUri = new Uri("http://www.google.com");
                var res = fac.Snif(myUri);
                var sut = res.Result.ToList();
                sut.Count().Should().Be(0);
            }
        }
        [Fact]
        public void sniff_python_with_list_of_uris() 
        {
            Uri myUri = new Uri("https://pypi.python.org/pypi/Python/json");
            List<Uri> myList = new List<Uri>();
            myList.Add(myUri);
            using (PypiFactory fac = new PypiFactory()) 
            {
                var res = fac.Snif(myList);
                var sut = res.Result.ToList();
                sut.Count().Should().BeGreaterThan(0);
            }
            
        }

        [Fact]
        public void sniff_python_with_bad_list_should_return_empty()
        {
            Uri myUri = new Uri("http://linuxfr.org");
            List<Uri> myList = new List<Uri>();
            myList.Add(myUri);
            using (PypiFactory fac = new PypiFactory())
            {
                var res = fac.Snif(myList);
                var sut = res.Result.ToList();
                sut.Count().Should().Be(0);
            }
        }

    }
}

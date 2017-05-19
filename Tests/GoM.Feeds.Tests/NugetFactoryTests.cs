using System;
using System.Collections.Generic;
using System.Text;
using GoM.Feeds.Results;
using GoM.Feeds;
using GoM.Feeds.Abstractions;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace GoM.Feeds.Tests
{
    public class NugetFactoryTests
    {

        [Fact]
        public void create_factory_works()
        {
            using (NugetOrgFactory sut = new NugetOrgFactory())
            {
                sut.Should().NotBeNull();
            }
            
        }

        [Fact]
        public void sniff_nuget_with_proper_single_uri()
        {
            using (NugetOrgFactory fac = new NugetOrgFactory())
            {
                Uri myUri = new Uri("http://api.nuget.org/v3/index.json");
                var res = fac.Snif(myUri);
                var sut = res.Result.ToList();
                sut[0].Result.Should().Be(true);
            }
            
        }

        [Fact]
        public void sniff_nuget_with_bad_single_uri_must_return_false()
        {
            using (NugetOrgFactory fac = new NugetOrgFactory())
            {
                Uri myUri = new Uri("http://www.google.com");
                var res = fac.Snif(myUri);
                var sut = res.Result.ToList();
                sut[0].Result.Should().Be(false);
            }  
        }
        [Fact]
        public void sniff_nuget_with_list_of_uris() 
        {
            Uri myUri = new Uri("http://api.nuget.org/v3/index.json");
            List<Uri> myList = new List<Uri>();
            myList.Add(myUri);
            using (NugetOrgFactory fac = new NugetOrgFactory())
            {
                var res = fac.Snif(myList);
                var sut = res.Result.ToList();
                sut[0].Result.Should().Be(true);
            }   
        }
        [Fact]
        public void sniff_nuget_with_bad_list_should_return_false()
        {
            Uri myUri = new Uri("http://linuxfr.org");
            List<Uri> myList = new List<Uri>();
            myList.Add(myUri);
            using (NugetOrgFactory fac = new NugetOrgFactory())
            {
                var res = fac.Snif(myList);
                var sut = res.Result.ToList();
                sut[0].Result.Should().Be(false);
            }
        }
    }
}

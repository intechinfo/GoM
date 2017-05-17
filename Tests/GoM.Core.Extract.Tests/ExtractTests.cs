using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GoM.Core.Extract.Tests
{
    public class ExtractTests
    {

        private String testPath = @"C:\Users\Nicolas\Test_Git2sharp";

        [Fact]
        public void test1()
        {
            ReadRepository rp = new ReadRepository(testPath);
            rp.isRepository().Should().Be(true);
        }
    }
}

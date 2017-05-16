using Xunit;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class PackageConfigParserTests
    {
        [Fact]
        public void PackageConfigParser() { 
            var p = new PhysicalFileInfo(new System.IO.FileInfo("call your own path to test"));
            var t = new PackageConfigParser(p);
            var k = t.Read();
        }
    }
}


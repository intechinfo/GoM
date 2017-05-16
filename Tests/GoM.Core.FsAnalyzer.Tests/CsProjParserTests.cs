using Xunit;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class CsProjParserTests
    {
        [Fact]
        public void CsProjParser()
        {
            var p = new PhysicalFileInfo(new System.IO.FileInfo("call your own path to test"));
            var t = new CsProjParser(p);
            var k = t.Read();
        }
    }
}

using System;
using System.IO;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace GoM.Core.Tests
{
    public class PhpComposerParserTests
    {
        [Fact]
        public void test_php_config_file()
        {
            PhpComposerParser pcp = new PhpComposerParser(@"C:\Dev\GoM\GoM.Core.Tests\Samples\");
            IEnumerable<ITarget> t = pcp.Read();
            List<ITarget> targets = t.ToList();
        }
    }
}

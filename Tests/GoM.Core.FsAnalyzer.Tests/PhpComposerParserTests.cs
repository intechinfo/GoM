using System;
using Xunit;
using Microsoft.Extensions.FileProviders;
using GoM.Core.FSAnalyzer.Utils;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using GoM.Core.Mutable;
using System.Linq;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class PhpComposerParserTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void test_php_config_file()
        {
            IFileProvider provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "Php"));
            IFileInfo file = provider.GetFileInfo( "sampleComposer.json" );

            PhpComposerParser pcp = new PhpComposerParser( file );
            var t = pcp.Read();
            List<Target> target = t.ToList();

            target.Should().HaveCount( 1, "Because we don't have targets in a web project." );
            var tDependencies = target.Select( e => e.Dependencies )
                                      .ToList();

            tDependencies[0].Should().HaveCount( 5, "We have 5 required dependencies" );
        }
    }
}

using System;
using Xunit;
using Microsoft.Extensions.FileProviders;
using GoM.Core.FSAnalyzer.Utils;
using FluentAssertions;
using System.Collections.Generic;
using GoM.Core.Mutable;
using System.Linq;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class PhpComposerParserTests
    {
        [Fact]
        public void test_php_config_file()
        {
            IFileProvider provider = new PhysicalFileProvider(@"C:\Dev\GoM\Samples\Php");
            IDirectoryContents contents = provider.GetDirectoryContents( String.Empty );
            IFileInfo file = provider.GetFileInfo( "samplePhpComposer.json" );

            PhpComposerParser pcp = new PhpComposerParser( file );
            var t = pcp.Read();
            List<Target> target = t.ToList();

            target.Should().HaveCount( 1, "Because we don't have targets in a web project." );
            var tDependencies = target.Select( e => e.Dependencies )
                                      .ToList();

            tDependencies[0].Should().HaveCount( 5, "We have 5 dependencies" );
        }
    }
}

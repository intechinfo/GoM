using System;
using Xunit;
using Microsoft.Extensions.FileProviders;
using GoM.Core.FSAnalyzer.Utils;
using FluentAssertions;
using System.Collections.Generic;
using System.IO;
using GoM.Core.Mutable;
using System.Linq;
using Microsoft.Extensions.FileProviders.Physical;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class PhpComposerParserTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void test_php_config_file_with_dependencies()
        {
            IFileProvider provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "Php"));
            IFileInfo file = provider.GetFileInfo( "composer.json" );

            PhpComposerParser pcp = new PhpComposerParser( file );
            var t = pcp.Read();
            List<Target> target = t.ToList();

            target.Should().HaveCount( 1, "Because we don't have targets in a web project." );
            var tDependencies = target.Select( e => e.Dependencies )
                                      .ToList();

            tDependencies[0].Should().HaveCount( 9, "We have 5 required dependencies and 4 dev dependencies" );
            tDependencies[ 0 ][ 0 ].Name.Should().Be( "php" );
            tDependencies[ 0 ][ 0 ].Version.Should().Be( "^5.3.6 || ^7.0" );
            tDependencies[ 0 ][ 1 ].Name.Should().Be( "clue/graph" );
            tDependencies[ 0 ][ 1 ].Version.Should().Be( "^0.9.0" );
            tDependencies[ 0 ][ 2 ].Name.Should().Be( "jms/composer-deps-analyzer" );
            tDependencies[ 0 ][ 2 ].Version.Should().Be( "0.1.*" );
            tDependencies[ 0 ][ 3 ].Name.Should().Be( "symfony/console" );
            tDependencies[ 0 ][ 3 ].Version.Should().Be( "^2.1 || ^3.0" );
            tDependencies[ 0 ][ 4 ].Name.Should().Be( "graphp/graphviz" );
            tDependencies[ 0 ][ 4 ].Version.Should().Be( "^0.2.0" );
            tDependencies[ 0 ][ 5 ].Name.Should().Be( "composer/composer" );
            tDependencies[ 0 ][ 5 ].Version.Should().Be( "~1.0.0" );
            tDependencies[ 0 ][ 6 ].Name.Should().Be( "jakub-onderka/php-parallel-lint" );
            tDependencies[ 0 ][ 6 ].Version.Should().Be( "~0.8" );
            tDependencies[ 0 ][ 7 ].Name.Should().Be( "phpunit/phpunit" );
            tDependencies[ 0 ][ 7 ].Version.Should().Be( "~4.8|~5.0" );
            tDependencies[ 0 ][ 8 ].Name.Should().Be( "squizlabs/php_codesniffer" );
            tDependencies[ 0 ][ 8 ].Version.Should().Be( "~2.1.0" );
        }
    }
}

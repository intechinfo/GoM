using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class PackageConfigParserTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_packagesconfig_parser_valid_file_return_correct_number_of_targets()
        { 
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "CSharp/PackagesConfig/packages.config")));
            var parser = new PackageConfigParser(file);
            var targets = parser.Read().ToList();

            targets.Should().HaveCount(1, "Only one target can be specified in a packages.config");
            targets[0].Dependencies.Should().HaveCount(3, "3 dependencies were specified in the sample package.config");
        }

        [Fact]
        public void Test_packagesconfig_parser_invalid_file_raises_exception()
        {
            var file = new PhysicalFileInfo(new FileInfo("Invalid/path"));
            var parser = new PackageConfigParser(file);
            parser.Invoking(x => x.Read().ToList())
                .ShouldThrow<DirectoryNotFoundException>();
        }
    }
}


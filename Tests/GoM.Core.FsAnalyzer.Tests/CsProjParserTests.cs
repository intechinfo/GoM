using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class CsProjParserTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_csproj_parser_valid_file_return_correct_number_of_targets()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "CSharp/Csproj/wander.csproj")));
            var parser = new CsProjParser(file);
            var targets = parser.Read().ToList();

            targets.Should().HaveCount(1, "Only one target is specified");
            targets[0].Dependencies.Should().HaveCount(4, "4 dependencies are specified in the csproj");
            Console.WriteLine(targets);
        }
    }
}

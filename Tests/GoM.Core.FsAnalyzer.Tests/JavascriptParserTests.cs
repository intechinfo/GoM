using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;
using Xunit;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class JavascriptParserTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_javascript_parser_valid_file_return_correct_number_of_targets()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Javascript/good_package/")));
            var parser = new JsPackageParser(file);
            var targets = parser.Read().ToList();
            targets.Should().HaveCount(1, "Only one target can be specified in a javascript file");
        }

        [Fact]
        public void Test_javascript_parser_valid_file_return_correct_number_of_dependencies()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Javascript/good_package/")));
            var parser = new JsPackageParser(file);
            var targets = parser.Read().ToList();

            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(54, "54 dependencies were specified in this json file");
        }

        [Fact]
        public void Test_javascript_parser_valid_file_return_correct_number_of_dependencies_when_no_dependencies()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Javascript/bad_package/no_dependencies")));
            var parser = new JsPackageParser(file);
            var targets = parser.Read().ToList();

            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(25, "25 dependencies were specified in this json file");
        }

        [Fact]
        public void Test_javascript_parser_valid_file_return_correct_number_of_dependencies_when_no_dev_dependencies()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Javascript/bad_package/no_dev_dependencies")));
            var parser = new JsPackageParser(file);
            var targets = parser.Read().ToList();

            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(29, "29 dependencies were specified in this json file");
        }

        [Fact]
        public void Test_javascript_parser_valid_file_return_no_declare_dependencies()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Javascript/bad_package/no_declare_dependencies")));
            var parser = new JsPackageParser(file);
            var targets = parser.Read().ToList();

            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(0, "0 dependencies were specified in this json file");
        }

        [Fact]
        public void Test_javascript_parser_valid_file_return_no_key_dependencies()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Javascript/bad_package/no_key_dependencies")));
            var parser = new JsPackageParser(file);
            var targets = parser.Read().ToList();

            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(0, "0 dependencies were specified in this json file");
        }

    }
}

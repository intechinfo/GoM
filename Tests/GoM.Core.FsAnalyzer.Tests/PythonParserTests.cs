﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;
using Xunit;
using FluentAssertions;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class PythonParserTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_python_parser_valid_file_return_correct_number_of_targets()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Python/setup.py")));
            var parser = new PythonSetupParser(file);
            var targets = parser.Read().ToList();
            targets.Should().HaveCount(1, "Only one target can be specified in a python setup file");

            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(5, "5 dependencies were specified in this setup file");
        }

        [Fact]
        public void Test_python_parser_valid_file_return_correct_target_names()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Python/setup.py")));
            var parser = new PythonSetupParser(file);
            var targets = parser.Read().ToList();
            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(5, "5 dependencies were specified in this setup file");

            var expected = new[] {"Test", "Werkzeug", "Jinja2", "itsdangerous", "click"};
            int i = 0;
            foreach (var dependency in dependencies)
            {
                dependency.Name.Should().Be(expected[i]);
                i++;
            }
        }

        [Fact]
        public void Test_python_parser_valid_file_return_correct_target_versions()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Python/setup.py")));
            var parser = new PythonSetupParser(file);
            var targets = parser.Read().ToList();
            var dependencies = targets[0].Dependencies.ToList();
            dependencies.Should().HaveCount(5, "5 dependencies were specified in this setup file");

            var expected = new[] { "", ">=0.7", ">=2.4", ">=0.21", ">=2.0" };
            int i = 0;
            foreach (var dependency in dependencies)
            {
                dependency.Version.Should().Be(expected[i]);
                i++;
            }
        }

        [Fact]
        public void Test_python_parser_invalid_file_return_empty_targets()
        {
            var file = new PhysicalFileInfo(new FileInfo(Path.Combine(SampleDirectory, "Python/invalid_setup.py")));
            var parser = new PythonSetupParser(file);
            var targets = parser.Read().ToList();
            targets.Should().HaveCount(0, "Invalid file should not return any target");
        }

        [Fact]
        public void Test_python_parser_unknown_file_raises_exception()
        {
            var file = new PhysicalFileInfo(new FileInfo("Invalid/Path"));
            var parser = new PythonSetupParser(file);
            parser.Invoking(x => x.Read().ToList())
                .ShouldThrow<DirectoryNotFoundException>();
        }
    }
}

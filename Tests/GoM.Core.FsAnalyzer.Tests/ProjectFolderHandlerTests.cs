using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using GoM.Core.FSAnalyzer;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class ProjectFolderHandlerTests : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_project_handler_return_csproj_handler_on_csproj_project()
        {
            var provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "Csharp/Csproj/"));
            var handler = new ProjectFolderHandler(provider);
            var result = handler.Sniff();

            result.Should().BeOfType(typeof(CsProjHandler));
        }

        [Fact]
        public void Test_project_handler_return_packagesconfig_handler_on_packagesconfig_project()
        {
            var provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "Csharp/PackagesConfig/"));
            var handler = new ProjectFolderHandler(provider);
            var result = handler.Sniff();

            result.Should().BeOfType(typeof(PackagesConfigHandler));
        }

        [Fact]
        public void Test_project_handler_return_python_handler_on_python_project()
        {
            var provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "Python/"));
            var handler = new ProjectFolderHandler(provider);
            var result = handler.Sniff();

            result.Should().BeOfType(typeof(PythonProjectHandler));
        }

        [Fact]
        public void Test_project_handler_return_php_handler_on_php_project()
        {
            var provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "Php/"));
            var handler = new ProjectFolderHandler(provider);
            var result = handler.Sniff();

            result.Should().BeOfType(typeof(PhpProjectHandler));
        }
    }
}

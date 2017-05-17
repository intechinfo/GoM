using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using GoM.Core.FSAnalyzer;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders.Physical;
using Xunit;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class ProjectFolderControllerTest : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_project_folder_handler_controller_analyze_csharp_project()
        {
            var file = new PhysicalFileInfo(new FileInfo(RootDirectory));
            ProjectFolderController projectHandlerController = new ProjectFolderController();
            System.Collections.Generic.IReadOnlyCollection<IProject> projects = projectHandlerController.Analyze(file.PhysicalPath);
            //Assert
            Assert.Equal(projects.Count, 3);
        }

        public void Test_project_folder_handler_controller_analyze_no_git_folder()
        {
            
        }
    }
}

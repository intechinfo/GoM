using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using FluentAssertions;
using GoM.Core.FSAnalyzer;
using GoM.Core.FSAnalyzer.Utils;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Xunit;

namespace GoM.Core.FsAnalyzer.Tests
{
    public class ProjectFolderControllerTest : BaseFsAnalyzerTest
    {
        [Fact]
        public void Test_project_folder_handler_controller_analyze_returns_right_git_projects_folder_number()
        {
            ProjectFolderController projectHandlerController = new ProjectFolderController();
            PhysicalFileProvider provider = new PhysicalFileProvider(Path.Combine(SampleDirectory, "FakeGitFolderEmptyProjects/"));
            IReadOnlyCollection<IProject> projects = projectHandlerController.Analyze(provider);
            //Assert
            Assert.Equal(projects.Count, 0);
        }
    }
}

using System;
using System.IO;
using GoM.Core.FSAnalyzer;
using Microsoft.Extensions.FileProviders;
using Xunit;

namespace GoM.Core.Tests
{
    public class CSharpProjectFolderHandlerTests
    {
        [Fact]
        public void Test1()
        {
            
            /*
            var provider = new PhysicalFileProvider(@"D:\Projects\Eval c#\ITI.DocLib.Model");
            var projectHandler = new CSharpProjectFolderHandler(provider);
            var res = projectHandler.Sniff();
            Console.WriteLine(Directory.GetCurrentDirectory());
            Assert.True(res);
            */
           
        }

        [Fact]
        public void test_project_folder_handler_analyze()
        {
            string path = @"D:\VS2017_Projets\GoM";
            ProjectFolderController projectHandlerController = new ProjectFolderController();
            System.Collections.Generic.IReadOnlyCollection<IProject> projects = projectHandlerController.Analyze(path);
            //Assert
            Assert.Equal(projects.Count, 4);
        }

        [Fact]
        public void test_project_folder_handler_analyze_no_folder_git()
        {
            string path = @"D:\VS2017_Projets\PacManFantome";
            ProjectFolderController projectHandlerController = new ProjectFolderController();
            System.Collections.Generic.IReadOnlyCollection<IProject> projects = projectHandlerController.Analyze(path);
            //Assert
            Assert.Equal(projects.Count, 0);
        }
    }
}

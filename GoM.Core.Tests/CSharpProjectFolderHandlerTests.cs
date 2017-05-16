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
            var provider = new PhysicalFileProvider(@"D:\Projects\Eval c#\ITI.DocLib.Model");
            var projectHandler = new ProjectFolderHandler(provider);
            var res = projectHandler.Sniff();
            Console.WriteLine(Directory.GetCurrentDirectory());
            Assert.True(true);
        }
    }
}

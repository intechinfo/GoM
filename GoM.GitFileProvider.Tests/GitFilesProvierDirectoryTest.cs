using FluentAssertions;
using NUnit.Framework;
using System.IO;

namespace GoM.GitFileProvider.Tests
{
    [TestFixture]
    public class GitFilesProvierDirectory
    {
        private string ProjectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

		[Test]
        public void Get_directory_with_no_parameters()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(@"commits\57792d71d0fa5b8da36c5b8b5b2bdcd78c6c1d2b\GoM.GitFileProvider\app.config");

            foreach (var item in rootDir)
            {
                item.Exists.Should().BeTrue();
                if (item.IsDirectory)
                {
                    item.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + item.Name + Path.DirectorySeparatorChar);

                }
                else
                    item.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + item.Name);
            }
        }
    }
}

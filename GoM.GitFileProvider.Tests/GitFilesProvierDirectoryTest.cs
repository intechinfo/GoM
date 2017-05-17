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
            var rootDir = git.GetDirectoryContents("");

            rootDir.Exists.Should().BeTrue();
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

        [Test]
        public void Get_all_branches_of_the_repository()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(@"branches");

            rootDir.Exists.Should().BeTrue();
            foreach (var item in rootDir)
            {
                var branchDir =  git.GetDirectoryContents(item.PhysicalPath + @"GoM.Core.Abstractions");
                branchDir.Exists.Should().BeTrue();
                foreach (var dir in branchDir)
                {
                    dir.Exists.Should().BeTrue();
                    if (dir.IsDirectory)
                    {
                        dir.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + item.Name + Path.DirectorySeparatorChar);

                    }
                    else if(dir.Name.Contains("cs"))
                    {
                        dir.Length.Should().BeGreaterThan(0);
                    }
                    else
                        dir.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + item.Name);
                }
            }
        }


    }
}

using FluentAssertions;
using NUnit.Framework;
using System.IO;

namespace GoM.GitFileProvider.Tests
{
    [TestFixture]
    public class GitFilesProvierDirectory
    {
        private string ProjectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;

		[Test]
        public void Get_directory_with_no_parameters()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents("");

            rootDir.Exists.Should().BeTrue();
            foreach (var item in rootDir)
            {
                item.Exists.Should().BeTrue();
                item.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + item.Name);
            }
        }
        [Test]
        public void Get_directory_with_branch_parram()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(@"branches\origin/perso_yazman\GoM.Core.Abstractions");

            rootDir.Exists.Should().BeTrue();
            foreach (var item in rootDir)
            {
                item.Exists.Should().BeTrue();
                item.PhysicalPath.Should().Be(ProjectRootPath + @"\GoM.Core.Abstractions\" + item.Name);
            }
        }
        [Test]
        public void Get_directory_with_head_parram()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var headDir = git.GetDirectoryContents(@"head\GoM.GitFileProvider");
            headDir.Exists.Should().BeTrue();
            foreach (var item in headDir)
            {
                item.Exists.Should().BeTrue();
                if (item.IsDirectory)
                {
                    item.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + item.Name);

                }
                else
                {
                    item.PhysicalPath.Should().Be(ProjectRootPath + Path.DirectorySeparatorChar + "GoM.GitFileProvider" + Path.DirectorySeparatorChar +item.Name);
                    item.Length.Should().BeGreaterThan(0);
                }
            }
        }
        [Test]
        public void Get_directory_with_commit_parram()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(@"commits\1921471fd36db781bef6833b4723f34afccd8d71\GoM.GitFileProvider");
            rootDir.Exists.Should().BeTrue();
            foreach (var item in rootDir)
            {
                item.Exists.Should().BeTrue();
                item.PhysicalPath.Should().Be(ProjectRootPath + @"\GoM.GitFileProvider\" + item.Name);

            }
        }
        [Test]
        public void Get_directory_with_tag_parram()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(@"tags\GitWatcher\GoM.GitFileProvider");
            rootDir.Exists.Should().BeTrue();
            foreach (var item in rootDir)
            {
                item.Exists.Should().BeTrue();
                item.PhysicalPath.Should().Be(ProjectRootPath + @"\GoM.GitFileProvider\" + item.Name);

            }
        }
        [Test]
        public void Get_directory_with_null_parameters()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(null);

            rootDir.Exists.Should().BeFalse();
        }

        [Test]
        public void GlobingPatern()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            var rootDir = git.GetDirectoryContents(@"branches\origin/perso_yazman\*");
            var file = git.GetFileInfo(@"branches\origin/perso_yazman\*");

            rootDir.Exists.Should().BeFalse();
            file.Exists.Should().BeFalse();
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

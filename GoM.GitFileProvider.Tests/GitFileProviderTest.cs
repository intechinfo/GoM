using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GoM.GitFileProvider.Tests
{
    [TestFixture]
    public class GitFileProviderTest
    {
        private string ProjectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [Test]
        public void Create_GitFileProvider_And_Tests_Correct_And_Incorrect_Git_Repository_Paths()
        {
            bool exist;
            string ProjectRootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            exist = (bool)typeof(GitFileProvider).GetField("_exist", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(git);
            exist.Should().Be(true);

            git = new GitFileProvider(ProjectRootPath + @"\");
            exist = (bool)typeof(GitFileProvider).GetField("_exist", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(git);
            exist.Should().Be(true);

            GitFileProvider git1 = new GitFileProvider(ProjectRootPath +  @"\.git");
            exist = (bool)typeof(GitFileProvider).GetField("_exist", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(git1);
            exist.Should().Be(true);

            GitFileProvider git2 = new GitFileProvider(ProjectRootPath + @"\.git\");
            exist = (bool)typeof(GitFileProvider).GetField("_exist", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(git2);
            exist.Should().Be(true);

            GitFileProvider git3 = new GitFileProvider(ProjectRootPath + @"\Wrong\Path\");
            exist = (bool)typeof(GitFileProvider).GetField("_exist", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(git3);
            exist.Should().Be(false);

            GitFileProvider git4 = new GitFileProvider(ProjectRootPath + @"\Wrong\Path\.git");
            exist = (bool)typeof(GitFileProvider).GetField("_exist", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(git4);
            exist.Should().Be(false);
        }

        [Test]
        public void FileInfo_Of_Null_String_Should_Return_Default_FileInfo_With_Name_Invalid()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            IFileInfo file = git.GetFileInfo(null);
            file.Should().NotBeNull();
            file.Name.Should().Be("Invalid");
            file.PhysicalPath.Should().Be(null);
            file.Length.Should().Be(-1);
            file.Exists.Should().Be(false);
        }

        [Test]
        public void FileInfo_Of_Invalid_String_Should_Return_Default_FileInfo_With_Name_Invalid()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            IFileInfo file = git.GetFileInfo("IdontExist");
            file.Should().NotBeNull();
            file.Name.Should().Be("Invalid");
            file.PhysicalPath.Should().Be(null);
            file.Length.Should().Be(-1);
            file.Exists.Should().Be(false);
        }

        [Test]
        public void FileInfo_Of_Root_Should_Return_FileInfoRefType_With_Name_Root_And_PhysicalPath_Equals_ProjectRootPath()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            IFileInfo rootInfo = git.GetFileInfo("");

            rootInfo.Exists.Should().BeFalse();
            rootInfo.Name.Should().Be("root");
            rootInfo.PhysicalPath.Should().Be(ProjectRootPath);
            rootInfo.Length.Should().Be(-1);
            rootInfo.LastModified.Should().Be(default(DateTimeOffset));
        }

        [Test]
        public void FileInfo_Of_Branches()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            IFileInfo branchesFile = git.GetFileInfo("branches");

            branchesFile.Exists.Should().BeFalse();
            branchesFile.Name.Should().Be("branches");
            branchesFile.PhysicalPath.Should().Be(ProjectRootPath+@"\branches");
            branchesFile.Length.Should().Be(-1);
            branchesFile.LastModified.Should().Be(default(DateTimeOffset));
        }

        [Test]
        public void FileInfo_Of_Specific_Branch_Called_By_Name()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            IFileInfo namedBranch = git.GetFileInfo(@"branches\perso_yazman");

            namedBranch.Exists.Should().BeTrue();
            namedBranch.Name.Should().Be("perso_yazman");
            namedBranch.PhysicalPath.Should().Be(ProjectRootPath+@"branches\perso_yazman");
            namedBranch.Length.Should().Be(-1);
            namedBranch.LastModified.Should().Be(default(DateTimeOffset));
        }

        [Test]
        public void FileInfo_Of_File_In_Specific_Branch()
        {
            GitFileProvider git = new GitFileProvider(ProjectRootPath);
            IFileInfo fileInBranch = git.GetFileInfo(@"branches\origin/perso-KKKMPT\GoM.GitFileProvider\app.config");

            fileInBranch.Exists.Should().BeTrue();
            fileInBranch.Name.Should().Be("app.config");
            fileInBranch.PhysicalPath.Should().Be(ProjectRootPath + @"\branches\origin/perso-KKKMPT\GoM.GitFileProvider\app.config");
            fileInBranch.Length.Should().BeGreaterThan(-1);
            fileInBranch.LastModified.Should().Be(default(DateTimeOffset));
        }

        [Test]
        public void FileInfo_Of_Dir_In_Specific_Branch()
        {

        }
    }
}

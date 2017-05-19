using System;
using LibGit2Sharp;
using System.Linq;
using NUnit.Framework;
using GoM.Core.Mutable;

namespace GoM.Core.GitExplorer.Tests
{
    [TestFixture]
    public class GitExplorerTests
    {
        public string url = "https://github.com/SimpleGitVersion/SGV-Net.git";

        [Test]
        public void Check_repository_is_loaded()
        {
            var repository = new Communicator(url).loadRepository(url);
            Assert.NotNull(repository);
            Assert.IsInstanceOf(typeof(Repository), repository);
        }

        [Test]
        public void Check_get_basicGitRepository()
        {
            Communicator communicator = new Communicator(url);
            Assert.IsInstanceOf(typeof(BasicGitRepository), communicator.getBasicGitRepository());
        }

        [Test]
        public void Check_get_all_branches()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getAllBranches().Count());
        }

        [Test]
        public void Check_files_are_picked_up()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getFiles().Count);
        }

        [Test]
        public void Check_directories_are_picked_up()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getFolders().Count);
        }

        [Test]
        public void Check_branches_names_are_provided()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getAllBranchesName());
        }

        [TestFixtureTearDown]
        public void Cleanup()
        {
            Helpers.DeleteDirectory("repos");
        }


    }
}

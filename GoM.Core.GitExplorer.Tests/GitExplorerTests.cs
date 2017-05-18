﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibGit2Sharp;
using GoM.Core.Mutable;
using System.Linq;

namespace GoM.Core.GitExplorer.Tests
{
    [TestClass]
    public class GitExplorerTests
    {
        public string url = "https://github.com/SimpleGitVersion/SGV-Net.git";

        [TestMethod]
        public void Check_repository_is_loaded()
        {
            Communicator communicator = new Communicator(url);

            Assert.IsInstanceOfType(communicator.loadRepository(), typeof(Repository));
        }

        [TestMethod]
        public void Check_get_basicGitRepository()
        {
            Communicator communicator = new Communicator(url);
            Assert.IsInstanceOfType(communicator.getBasicGitRepository(), typeof(BasicGitRepository));
        }

        [TestMethod]
        public void Check_get_all_branches()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getAllBranches().Count);
        }

        [TestMethod]
        public void Check_files_are_picked_up()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getFiles().Count);
        }

        [TestMethod]
        public void Check_directories_are_picked_up()
        {
            Communicator communicator = new Communicator(url);
            Assert.AreNotEqual(0, communicator.getFolders().Count);
        }

        [TestCleanup]
        public void CleanTests()
        {
            Helpers.DeleteDirectory("repos");
        }
    }
}

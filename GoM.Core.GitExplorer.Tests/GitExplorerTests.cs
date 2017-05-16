using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibGit2Sharp;
using GoM.Core.GitExplorer;
using System.Collections.Generic;

namespace GoM.Core.GitExplorer.Tests
{
    [TestClass]
    public class GitExplorerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Communicator communicator = new Communicator("https://github.com/bmgm/Simple.git");

            Assert.IsInstanceOfType(communicator.loadRepository(), typeof(Repository));
            // Delete repos direct
            Helpers.DeleteDirectory(communicator.ReposPath);
        }
    }
}

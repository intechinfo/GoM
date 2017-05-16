using System;
using Xunit;
using FluentAssertions;

namespace GoM.Core.Extract.Tests
{
    class ExctractTests
    {

        private String pathTest = @"C:\Users\Nicolas\Test_Git2sharp";

        [Fact]
        public void folderIsGitRepository()
        {

            ReadProject rp = new ReadProject(this.pathTest);
            rp.gitRepositoryExist().Should().Be(true);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
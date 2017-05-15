using FluentAssertions;
using Microsoft.Extensions.FileProviders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.GitFileProvider.Tests
{
    [TestFixture]
    public class GitFileProviderTest
    {
        [Test]
        public void Create_GitFilesProvide()
        {
            GitFileProvider git = new GitFileProvider(@"D:\Ecole\S7\GoM");
            GitFileProvider git1 = new GitFileProvider(@"D:\Ecole\S7\GoM\.git");
            GitFileProvider git2 = new GitFileProvider(@"D:\Ecole\S7\");
        }
    }
}

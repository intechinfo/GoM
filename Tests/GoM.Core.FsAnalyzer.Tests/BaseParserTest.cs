using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GoM.Core.FsAnalyzer.Tests
{
    public abstract class BaseParserTest
    {
        public string RootDirectory => Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory())
            .Parent.Parent.Parent.Parent.FullName);

        public string SampleDirectory => Path.Combine(RootDirectory, "samples");
    }
}

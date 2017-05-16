using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class CsProjParser
    {
        public string Path { get; }

        public CsProjParser(string path)
        {
            Path = path;
        }

        public IEnumerable<ITarget> Read()
        {
            throw new NotImplementedException();
        }
    }
}

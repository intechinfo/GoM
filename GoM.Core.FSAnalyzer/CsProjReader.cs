using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core.Abstractions;

namespace GoM.Core.FSAnalyzer
{
    public class CsProjReader : IProjectConfigReader
    {
        public IEnumerable<ITarget> Targets { get; }
        private string _path;

        public CsProjReader(string path)
        {
            _path = path;
        }
        public void Read()
        {
            throw new NotImplementedException();
        }
    }
}

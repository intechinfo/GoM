using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.FSAnalyzer.Utils
{
    public abstract class BaseConfigParser
    {
        public string Path { get; }

        protected BaseConfigParser(string path)
        {
            Path = path;
        }

        public virtual IEnumerable<ITarget> Read()
        {
            throw new NotImplementedException();
        }
    }
}

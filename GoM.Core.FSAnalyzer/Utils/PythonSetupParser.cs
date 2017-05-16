using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class PythonSetupParser : BaseConfigParser
    {
        public PythonSetupParser(string path) : base(path)
        {
        }

        public override IEnumerable<ITarget> Read()
        {
            throw new NotImplementedException();
        }
    }
}

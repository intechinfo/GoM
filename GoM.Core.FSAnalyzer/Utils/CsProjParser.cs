using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class CsProjParser : BaseConfigParser
    {
        public CsProjParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<ITarget> Read()
        {
            throw new NotImplementedException();
        }
    }
}

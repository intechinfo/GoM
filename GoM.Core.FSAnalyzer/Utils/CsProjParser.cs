﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class CsProjParser : BaseConfigParser
    {
        public CsProjParser(string path) : base(path)
        {
        }

        public override IEnumerable<ITarget> Read()
        {
            throw new NotImplementedException();
        }
    }
}

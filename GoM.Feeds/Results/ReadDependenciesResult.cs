using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class ReadDependenciesResult
    {
        public readonly IEnumerable<TargetResult> Result;
        public readonly Exception Error;
        public bool Success => Error == null;
        public ReadDependenciesResult(Exception rE, IEnumerable<TargetResult> r)
        {
            Result = r;
            Error = rE;
        }
    }
}

using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class TargetResult
    {
        public readonly ITarget Result;

        public readonly Exception Reason;

        public bool Success => Result != null;

        public TargetResult(Exception rE, ITarget r)
        {
            Result = r;
            Reason = rE;
        }
    }
}

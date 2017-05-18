using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class GetPackagesResult
    {
        
        public readonly IEnumerable<TargetResult> Result;

        public readonly Exception Reason;

        public bool Success => Result != null;

        public GetPackagesResult(Exception rE, IEnumerable<TargetResult> r)
        {
            Result = r;
            Reason = rE;
        }
    }
}

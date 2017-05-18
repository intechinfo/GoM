using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class GetDependenciesResult
    {
        public readonly IEnumerable<GetDependenciesResult> Result;

        public readonly Exception Reason;

        public bool Success => Result != null;

        public GetDependenciesResult(Exception rE, IEnumerable<GetDependenciesResult> r)
        {
            Result = r;
            Reason = rE;
        }
    }
}

using System;
using System.Collections.Generic;

namespace GoM.Feeds.Results
{
    public class GetPackagesResult
    {
        
        public readonly IEnumerable<PackageInstanceResult> Result;

        public readonly Exception Reason;

        public bool Success => Result != null;

        public GetPackagesResult(Exception rE, IEnumerable<PackageInstanceResult> r)
        {
            Result = r;
            Reason = rE;
        }
    }
}

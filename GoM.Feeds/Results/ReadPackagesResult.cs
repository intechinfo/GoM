using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class ReadPackagesResult
    {
        public readonly IEnumerable<PackageInstanceResult> Result;
        public readonly Exception Error;
        public bool Success => Error == null;
        public ReadPackagesResult(Exception rE, IEnumerable<PackageInstanceResult> r)
        {
            Result = r;
            Error = rE;
        }
    }
}

using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class PackageInstanceResult
    {
        public readonly IPackageInstance Result;

        public readonly Exception Reason;

        public bool Success => Result != null;

        public PackageInstanceResult(Exception rE, IPackageInstance r)
        {
            Result = r;
            Reason = rE;
        }
    }
}

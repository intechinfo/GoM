using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Feeds.Results
{
    public class GetPackagesResult
    {

        public readonly IDictionary<IPackageInstance, IEnumerable<PackageInstanceResult>> Result;
        public readonly IEnumerable<FeedMatchResult> FeedErrors;

        public readonly Exception Reason;

        public bool Success => Result != null;

        public GetPackagesResult(Exception rE, IDictionary<IPackageInstance, IEnumerable<PackageInstanceResult>> r, IEnumerable<FeedMatchResult> fE)
        {
            Result = r;
            Reason = rE;
            FeedErrors = fE;
        }
    }
}

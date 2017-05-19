using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class ReadPackagesResult
    {
        public readonly IEnumerable<PackageInstanceResult> Result;

        public readonly JsonResult Json;

        public readonly Exception Error;
        public bool Success => Error == null;
        public ReadPackagesResult(Exception rE, IEnumerable<PackageInstanceResult> r, JsonResult j)
        {
            Result = r;
            Error = rE;
            Json = j;
        }
    }
}

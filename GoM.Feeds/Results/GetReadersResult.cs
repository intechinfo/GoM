using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class GetReadersResult
    {
        public readonly IEnumerable<IFeedReader> Result;
        public bool Succes => Result != null;
        public IEnumerable<Exception> Reasons;
        public GetReadersResult(IEnumerable<Exception> e, IEnumerable<IFeedReader> r)
        {
            Result = r;
            Reasons = e;
        }
    }
}

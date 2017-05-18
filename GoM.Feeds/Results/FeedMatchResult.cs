using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class FeedMatchResult
    {
        public readonly bool Result;

        public readonly Exception Reason;
        public bool Success => Reason == null;

        public FeedMatchResult(Exception rE, bool r)
        {
            Result = r;
            Reason = rE;
        }
    }
}

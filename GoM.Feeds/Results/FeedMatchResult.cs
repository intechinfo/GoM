using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds.Results
{
    public class FeedMatchResult
    {
        public readonly bool Result;
        public readonly Exception Error;
        public bool Success => Error == null;
        public readonly JsonResult Json;
        public readonly IFeedReader FeedReader;
        public FeedMatchResult(Exception rE, bool r, JsonResult j,IFeedReader f)
        {
            Result = r;
            Error = rE;
            Json = j;
            FeedReader = f;
        }
    }
}

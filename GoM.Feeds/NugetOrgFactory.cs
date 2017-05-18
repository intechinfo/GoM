using GoM.Feeds.Abstractions;
using GoM.Feeds.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoM.Feeds
{
    public class NugetOrgFactory : IFeedFactory
    {
        NugetOrgFeedReader _feedReader;
        public NugetOrgFactory()
        {
            _feedReader = new NugetOrgFeedReader();
        }
        public void Dispose()
        {
            _feedReader.Dispose();
        }
        public IEnumerable<IFeedReader> FeedReaders => new List<IFeedReader> { _feedReader };

        public GetReadersResult Snif(IEnumerable<Uri> links)
        {
            var t = links.Select(x => Snif(x));
            return new GetReadersResult(t.SelectMany(x => x.Reasons), t.SelectMany(x => x.Result));
        }

        public GetReadersResult Snif(Uri link)
        {
            var res = _feedReader.FeedMatch(link).Result;
            List<IFeedReader> list = null;
            List<Exception> exs = null;
            if (res.Result && res.Success)
            {
                list = new List<IFeedReader> { _feedReader };
            }
            else if (res.Success && !res.Result)
            {
                list = new List<IFeedReader>();
            }
            else
            {
                exs = new List<Exception> { res.Reason };
            }
            return new GetReadersResult(exs, list);
        }
    }
}

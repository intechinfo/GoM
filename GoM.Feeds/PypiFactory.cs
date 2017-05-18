using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoM.Feeds
{
    public class PypiFactory : IFeedFactory
    {
        PypiOrgFeedReader _feedReader;
        public PypiFactory()
        {
            _feedReader = new PypiOrgFeedReader();
        }
        public void Dispose()
        {
            _feedReader.Dispose();
        }
        public IEnumerable<IFeedReader> FeedReaders => new List<IFeedReader> { _feedReader };

        public IEnumerable<IFeedReader> Snif(IEnumerable<Uri> links)
        {
            return  links.SelectMany(x => Snif(x));
        }

        public IEnumerable<IFeedReader> Snif(Uri link)
        {
            var list = new List<IFeedReader>();
            var res = _feedReader.FeedMatch(link).Result;
            var fr = res.Success && res.Result ? _feedReader : null;
            if (fr != null) list.Add(fr);
            return list;
        }
    }
}

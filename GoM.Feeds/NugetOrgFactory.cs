using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoM.Feeds
{
    internal class NugetOrgFactory : IFeedFactory
    {
        NugetOrgFeedReader _feedReader;
        public NugetOrgFactory()
        {
            _feedReader = new NugetOrgFeedReader();
        }
        public IEnumerable<IFeedReader> FeedReaders => new List<IFeedReader> { _feedReader };

        public IEnumerable<IFeedReader> Snif(List<Uri> links)
        {
            return links.SelectMany(x => Snif(x));
        }

        public IEnumerable<IFeedReader> Snif(Uri link)
        {
            var list = new List<IFeedReader>();
            var fr = _feedReader.FeedMatch(link).Result ? _feedReader : null;
            if (fr != null) list.Add(fr);
            return list;
        }
    }
}

﻿using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Linq;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class PypiFactory : IFeedFactory
    {
        PypiOrgFeedReader _feedReader;
        public PypiFactory()
        {
            _feedReader = new PypiOrgFeedReader();
        }
        public IEnumerable<IFeedReader> FeedReaders => new List<IFeedReader> { _feedReader };

        public IEnumerable<IFeedReader> Snif(IEnumerable<Uri> links)
        {
            return  links.SelectMany(x => Snif(x));
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

﻿using GoM.Feeds.Abstractions;
using GoM.Feeds.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public sealed class DefaultFeedFactory : IFeedFactory
    {
        List<IFeedFactory> _factories;
        public DefaultFeedFactory()
        {
            _factories = new List<IFeedFactory>
            {
                new NpmJsFactory(),
                new PypiFactory(),
                new NugetOrgFactory()
            };
        }
        public void Dispose()
        {
            _factories.ForEach(x => x.Dispose());
        }
        public IEnumerable<IFeedReader> FeedReaders => _factories.SelectMany(x => x.FeedReaders);

        public async Task<IEnumerable<FeedMatchResult>> Snif(IEnumerable<Uri> links)
        {
            var t = links.Select(x => Snif(x));
            var ret = await Task.WhenAll(t.Select(x => x));
            return ret.SelectMany(x=>x);
        }
        public async Task<IEnumerable<FeedMatchResult>> Snif(Uri link)
        {
            var ret = await Task.WhenAll(_factories.Select(fr => fr.Snif(link)));
            return ret.SelectMany(x=>x);
        }

    }
}

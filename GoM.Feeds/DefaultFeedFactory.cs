using GoM.Feeds.Abstractions;
using GoM.Feeds.Results;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public GetReadersResult Snif(IEnumerable<Uri> links)
        {
            var t = links.Select(x => Snif(x));
            return new GetReadersResult(t.SelectMany(x => x.Reasons), t.SelectMany(x => x.Result));
        }
        public GetReadersResult Snif(Uri link)
        {
            var t = _factories.Select(fr => fr.Snif(link));
            return new GetReadersResult(t.SelectMany(x => x.Reasons), t.SelectMany(x => x.Result));
        }

    }
}

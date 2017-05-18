using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;

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

        public IEnumerable<IFeedReader> Snif(IEnumerable<Uri> links)
        {
            return links.SelectMany(l => Snif(l));
        }
        public IEnumerable<IFeedReader> Snif(Uri link)
        {
           return _factories.SelectMany(f => f.FeedReaders).Where( (fr) => { var res = fr.FeedMatch(link).Result; return res.Success && res.Result; });
        }
    }
}

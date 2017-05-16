using System;
using System.Collections.Generic;
using GoM.Core;
using GoM.Feeds.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class FeedManager : IFeedManager
    {
        public IFeedFactory _factory;
        private IReadOnlyCollection<IFeedReader> _readers;

        public FeedManager()
        {
            _factory = new DefaultFeedFactory();   
        }

        public IDictionary<IPackageInstance, IEnumerable<IPackageInstance>> GetNewestVersions(List<IPackageFeed> packageFeeds, List<IPackageInstance> packages)
        {
            IEnumerable<IFeedReader> feeds = _factory.Snif(packageFeeds);
            var toDo = packages.Join(feeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.GetNewestVersions(p.Name, p.Version) } );
            Task.WaitAll(toDo.Select(x => x.T).ToArray());
            return toDo.ToDictionary(x => x.P, x => x.T.Result);
        }
    }
}
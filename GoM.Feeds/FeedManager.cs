using GoM.Core;
using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class FeedManager : IFeedManager
    {
        IFeedFactory _factory;

        public FeedManager()
        {
            _factory = new DefaultFeedFactory();   
        }
        public void Dispose()
        {
            _factory.Dispose();
        }
        public IDictionary<IPackageInstance, IEnumerable<IPackageInstance>> GetAllVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages)
        {
            IEnumerable<IFeedReader> feeds = _factory.Snif(packageFeeds);
            var toDo = packages.Join(feeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.GetAllVersions(p.Name) });
            Task.WaitAll(toDo.Select(x => x.T).ToArray());
            return toDo.ToDictionary(x => x.P, x => x.T.Result);
        }

        public IDictionary<IPackageInstance, IEnumerable<IPackageInstance>> GetNewestVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages)
        {
            IEnumerable<IFeedReader> feeds = _factory.Snif(packageFeeds);
            var toDo = packages.Join(feeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.GetNewestVersions(p.Name, p.Version) } );
            Task.WaitAll(toDo.Select(x => x.T).ToArray());
            return toDo.ToDictionary(x => x.P, x => x.T.Result);
        }
    }
}
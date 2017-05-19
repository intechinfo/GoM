using GoM.Core;
using GoM.Feeds.Abstractions;
using GoM.Feeds.Results;
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
        public GetPackagesResult GetAllVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages)
        {
            var feedsResults = _factory.Snif(packageFeeds).Result;

            var validFeeds = feedsResults.Where(x => x.Success);
            var invalidFeeds = feedsResults.Where(x => !x.Success);

            var toDo = packages.Join(validFeeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.FeedReader.GetAllVersions(p.Name) });

            Task.WaitAll(toDo.Select(x => x.T).ToArray());
            var ret = new GetPackagesResult(null, toDo.ToDictionary(x => x.P, x => x.T.Result.Result), invalidFeeds);
            return ret;
        }
        public GetPackagesResult GetNewestVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages)
        {
            var feedsResults = _factory.Snif(packageFeeds).Result;
            var validFeeds = feedsResults.Where(x => x.Success);
            var invalidFeeds = feedsResults.Where(x => !x.Success);


            var toDo = packages.Join(validFeeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.FeedReader.GetNewestVersions(p.Name, p.Version) } );
            Task.WaitAll(toDo.Select(x => x.T).ToArray());
             
            var ret = new GetPackagesResult(null, toDo.ToDictionary(x => x.P, x => x.T.Result.Result), invalidFeeds);
            return ret;
        }
    }
}
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="packageFeeds"></param>
        /// <param name="packages"></param>
        /// <returns></returns>
        public GetPackagesResult GetAllVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages)
        {
            var feedsResults = _factory.Snif(packageFeeds).Result;

            var validFeeds = feedsResults.Where(x => x.Success);
            var invalidFeeds = feedsResults.Where(x => !x.Success);

            var toDo = packages.Join(validFeeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.FeedReader.GetAllVersions(p.Name) });

            Task.WaitAll(toDo.Select(x => x.T).ToArray());
            var toDicData = toDo.GroupBy(x => x.P);
            
            return new GetPackagesResult(null, toDicData.ToDictionary(x => x.Key, x => x.SelectMany(z=>z.T.Result.Result)), invalidFeeds);
        }
        public GetPackagesResult GetNewestVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages)
        {
            var feedsResults = _factory.Snif(packageFeeds).Result;
            var validFeeds = feedsResults.Where(x => x.Success);
            var invalidFeeds = feedsResults.Where(x => !x.Success);


            var toDo = packages.Join(validFeeds, p => 1, f => 1, (p, f) => new { P = p, F = f, T = f.FeedReader.GetNewestVersions(p.Name, p.Version) } );
            Task.WaitAll(toDo.Select(x => x.T).ToArray());
            var toDicData = toDo.GroupBy(x => x.P);
            return new GetPackagesResult(null, toDicData.ToDictionary(x => x.Key, x => x.SelectMany(z => z.T.Result.Result)), invalidFeeds);
        }
    }
}
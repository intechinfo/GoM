using GoM.Feeds.Abstractions;
using GoM.Feeds.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoM.Feeds
{
    public class NugetOrgFactory : IFeedFactory
    {
        NugetOrgFeedReader _feedReader;
        public NugetOrgFactory()
        {
            _feedReader = new NugetOrgFeedReader();
        }
        public void Dispose()
        {
            _feedReader.Dispose();
        }
        /// <summary>
        /// member : List of FeedReaders
        /// </summary>
        public IEnumerable<IFeedReader> FeedReaders => new List<IFeedReader> { _feedReader };
        /// <summary>
        /// Given a collection of Uri, detects which FeedReader to use 
        /// </summary>
        /// <param name="links">collection of uris</param>
        /// <returns>Task of collection of FeedMatchResult</returns>
        public async Task<IEnumerable<FeedMatchResult>> Snif(IEnumerable<Uri> links)
        {
            var t = links.Select(x => Snif(x));
            var ret = (await Task.WhenAll(t.Select(x => x))).SelectMany(x => x);
            return ret;
        }
        /// <summary>
        /// Given an Uri, detects which FeedReader to use
        /// </summary>
        /// <param name="link">single uri</param>
        /// <returns>task of collection of FeedMatchResult</returns>
        public async Task<IEnumerable<FeedMatchResult>> Snif(Uri link)
        {
            return new List<FeedMatchResult> { await _feedReader.FeedMatch(link) };
        }
    }
}

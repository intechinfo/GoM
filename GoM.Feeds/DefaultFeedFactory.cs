using GoM.Feeds.Abstractions;
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
        /// <summary>
        /// Creates  a factory
        /// </summary>
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

        /// <summary>
        /// Detects which feed to use depending on the collection of links you input
        /// </summary>
        /// <param name="links"> collection of links</param>
        /// <returns>list of tasks</returns>
        public async Task<IEnumerable<FeedMatchResult>> Snif(IEnumerable<Uri> links)
        {
            var t = links.Select(x => Snif(x));
            var ret = await Task.WhenAll(t.Select(x => x));
            return ret.SelectMany(x=>x);
        }
        /// <summary>
        /// Detects which feed to use depending on the SINGLE link you input
        /// </summary>
        /// <param name="link"></param>
        /// <returns>task</returns>
        public async Task<IEnumerable<FeedMatchResult>> Snif(Uri link)
        {
            var ret = await Task.WhenAll(_factories.Select(fr => fr.Snif(link)));
            return ret.SelectMany(x=>x);
        }

    }
}

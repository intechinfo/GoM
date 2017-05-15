using System;
using System.Collections.Generic;
using GoM.Feeds.Abstractions;

namespace GoM.Feeds
{
    public class FeedManager : IFeedManager
    {
        public IReadOnlyCollection<IFeedFactory> _factories;
        private IReadOnlyCollection<IFeedReader> _readers;

        public FeedManager()
        {
            
        }

        public void Register(IFeedFactory factory)
        {
            
        }

        public void Sniff(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
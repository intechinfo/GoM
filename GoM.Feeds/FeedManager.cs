using System;
using System.Collections.Generic;
using GoM.Feeds.Abstractions;

namespace GoM.Feeds
{
    public class FeedManager : IFeedManager
    {
        public IFeedFactory _factory;
        private IReadOnlyCollection<IFeedReader> _readers;

        public FeedManager()
        {
            //_factory = "zob";    
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
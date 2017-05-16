using System;
using GoM.Feeds.Abstractions;

namespace GoM.Feeds 
{
    public class FeedFactory : IFeedFactory
    {

        public FeedFactory() 
        {
            
        }
        public string SniffingKeyword 
        { 
            get 
            {
                return this.SniffingKeyword;
            }
        }
        public IFeedReader GetInstance()
        {
            throw new NotImplementedException();
        }
    }
}
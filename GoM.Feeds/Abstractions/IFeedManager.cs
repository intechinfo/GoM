using System;


namespace GoM.Feeds.Abstractions
{
    public interface IFeedManager
    { 
        void Register(IFeedFactory factory);
       
        void Sniff(Uri uri);
    }
}
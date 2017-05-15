using System;


namespace GoM.Feeds.Abstractions
{
    public interface IFeedFactory
    {
        IFeedReader GetInstance();
        string SniffingKeyword { get; }
    }
}
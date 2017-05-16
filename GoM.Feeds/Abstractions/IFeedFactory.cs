using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedFactory
    {
        IFeedReader GetFeedReader();
        IEnumerable<IFeedReader> Snif(List<IPackageFeed> packages);
        IFeedReader Snif(IPackageFeed package);
    }
}
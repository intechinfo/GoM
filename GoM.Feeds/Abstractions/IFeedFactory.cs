using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedFactory
    {
        IFeedReader GetFeedReader();
        IEnumerable<IFeedReader> Snif(List<Uri> links);
        IFeedReader Snif(Uri link);
    }
}
using GoM.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedFactory
    {
        IEnumerable<IFeedReader> FeedReaders { get; }

        IEnumerable<IFeedReader> Snif(List<Uri> links);
        IEnumerable<IFeedReader> Snif(Uri link);
    }
}
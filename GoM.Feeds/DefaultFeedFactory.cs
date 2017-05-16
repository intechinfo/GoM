using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;

namespace GoM.Feeds
{
    public sealed class DefaultFeedFactory : IFeedFactory
    {
        public IFeedReader GetFeedReader()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<IFeedReader> Snif(List<IPackageFeed> packages)
        {
            throw new NotImplementedException();
        }

        public IFeedReader Snif(IPackageFeed package)
        {
            throw new NotImplementedException();
        }
    }
}

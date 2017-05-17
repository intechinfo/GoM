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
        public IEnumerable<IFeedReader> Snif(List<Uri> links)
        {
            throw new NotImplementedException();
        }

        public IFeedReader Snif(Uri link)
        {
            throw new NotImplementedException();
        }
    }
}

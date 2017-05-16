using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds
{
    internal class PypiFactory : IFeedFactory
    {
        public IFeedReader GetFeedReader()
        {
            throw new NotImplementedException();
        }

        public IFeedFactory Snif()
        {
            throw new NotImplementedException();
        }
    }
}

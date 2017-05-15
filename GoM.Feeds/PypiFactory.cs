using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds
{
    internal class PypiFactory : IFeedFactory
    {
        public string SniffingKeyword => "pypi.python.org";

        public IFeedReader GetInstance()
        {
            return new PypiFeedReader();
        }
    }
}

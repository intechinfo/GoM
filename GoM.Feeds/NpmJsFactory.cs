using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds
{
    internal class NpmJsFactory : IFeedFactory
    {
        public string SniffingKeyword => "npmjs.com";

        public IFeedReader GetInstance()
        {
            return new NpmJsFeedReader();
        }
    }
}

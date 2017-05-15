using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Feeds
{
    internal class NugetOrgFactory : IFeedFactory
    {
        public string SniffingKeyword => "nuget.org";

        public IFeedReader GetInstance()
        {
            return new NugetOrgFeedReader();
        }
    }
}

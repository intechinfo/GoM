using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;

namespace GoM.Feeds
{
    internal class NugetOrgFeedReader : NugetFeedReader
    {
        public override IEnumerable<IPackageInstance> GetAllVersions(string name)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<ITarget> GetDependencies(string name, string version)
        {
            throw new NotImplementedException();
        }
    }
}

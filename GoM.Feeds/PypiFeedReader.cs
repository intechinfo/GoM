using GoM.Core;
using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;

namespace GoM.Feeds 
{
    internal class PypiFeedReader : NpmFeedReader
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
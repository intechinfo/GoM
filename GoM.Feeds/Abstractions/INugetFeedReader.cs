using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;

namespace GoM.Feeds.Abstractions
{
    abstract class NugetFeedReader : IFeedReader
    {
        public abstract IEnumerable<IPackageInstance> GetAllVersions(string name);
        public abstract IEnumerable<ITarget> GetDependencies(string name, string version);
    }
}

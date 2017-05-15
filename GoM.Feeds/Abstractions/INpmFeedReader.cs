using GoM.Core;
using System.Collections.Generic;

namespace GoM.Feeds.Abstractions
{
    abstract class NpmFeedReader : IFeedReader
    {
        public abstract IEnumerable<IPackageInstance> GetAllVersions(string name);
        public abstract IEnumerable<ITarget> GetDependencies(string name, string version);
    }
}

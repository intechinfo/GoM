using GoM.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace GoM.Feeds.Abstractions
{
    public abstract class NpmFeedReader : IFeedReader
    {
        public abstract Task<bool> FeedMatch(Uri adress);
        public abstract Task<IEnumerable<IPackageInstance>> GetAllVersions(string name);
        public abstract Task<IEnumerable<ITarget>> GetDependencies(string name, string version);
        public abstract Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name, string version);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    abstract class NugetFeedReader : IFeedReader
    {
        public abstract Task<bool> FeedMatch(Uri adress);
        public abstract Task<IEnumerable<IPackageInstance>> GetAllVersions(string name);
        public abstract Task<IEnumerable<ITarget>> GetDependencies(string name, string version);
        public abstract Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name, string version);

    }
}

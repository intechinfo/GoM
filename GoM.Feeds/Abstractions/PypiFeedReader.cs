using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoM.Core;

namespace GoM.Feeds.Abstractions
{
    abstract class PypiFeedReader : IFeedReader
    {
        public abstract Task<IEnumerable<IPackageInstance>> GetAllVersions(string name);
        public abstract Task<IEnumerable<ITarget>> GetDependencies(string name, string version);
        public abstract Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name, string version);
    }
}
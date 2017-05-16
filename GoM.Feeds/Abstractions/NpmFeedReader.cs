using GoM.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    abstract class NpmFeedReader : IFeedReader
    {
        public abstract string BaseUrl { get; }
        public abstract Task<IEnumerable<IPackageInstance>> GetAllVersions(string name);
        public abstract Task<IEnumerable<ITarget>> GetDependencies(string name, string version);
    }
}

using GoM.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedManager
    {
       Task<IDictionary<IPackageInstance,List<IPackageInstance>>> GetNewestVersions(List<IPackageFeed> packageFeeds, List<IPackageInstance> packages);
    }
}
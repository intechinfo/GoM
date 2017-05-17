using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedManager
    {
       IDictionary<IPackageInstance,IEnumerable<IPackageInstance>> GetNewestVersions(List<Uri> packageFeeds, List<IPackageInstance> packages);
    }
}
using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Persistence
{
    public class PackageFeed : IPackageFeed
    {
        public Uri Url { get; }

        public List<PackageInstance> Packages { get; } 

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;

        public PackageFeed(Uri url)
        {
            Url = url;
            Packages = new List<PackageInstance>();
        }
    }
}
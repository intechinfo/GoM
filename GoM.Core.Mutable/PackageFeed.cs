using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class PackageFeed : IPackageFeed
    {
        public PackageFeed()
        {
        }

        /// <summary>
        /// Creates a Mutable PackageFeed from an existing PackageFeed, ie from an Immutable PackageFeed
        /// </summary>
        /// <param name="feed"></param>
        public PackageFeed(IPackageFeed feed)
        {
            Url = feed.Url;
            Packages = (List<PackageInstance>)feed.Packages;
        }

        public Uri Url { get; set; }

        public List<PackageInstance> Packages { get; } = new List<PackageInstance>();

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;
    }
}
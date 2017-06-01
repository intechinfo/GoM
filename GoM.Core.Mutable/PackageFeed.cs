using System;
using System.Collections.Generic;
using System.Linq;

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
            Packages = feed is PackageFeed ? (List<PackageInstance>)feed.Packages : new List<PackageInstance>(feed.Packages.Select(x => new PackageInstance(x)));
        }

        public Uri Url { get; set; }

        public List<PackageInstance> Packages { get; } = new List<PackageInstance>();

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;
    }
}
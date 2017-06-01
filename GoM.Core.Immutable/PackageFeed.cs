using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GoM.Core.Immutable
{
    public class PackageFeed : IPackageFeed
    {
        PackageFeed(Uri url, ImmutableList<PackageInstance> packages)
        {
            Packages = packages ?? throw new ArgumentException(nameof(packages));
            Url = url ?? throw new ArgumentException(nameof(url));
        }

        PackageFeed(IPackageFeed packageFeed)
        {
            Url = packageFeed.Url ?? throw new ArgumentException(nameof(packageFeed.Url));
            Packages = packageFeed.Packages == null ? throw new ArgumentException(nameof(packageFeed.Packages))
                : (ImmutableList<PackageInstance>)packageFeed.Packages;
        }

        public Uri Url { get; }

        public ImmutableList<PackageInstance> Packages { get; } = ImmutableList.Create<PackageInstance>();

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;

        public static PackageFeed Create(Uri url, ImmutableList<PackageInstance> packages)
        {
            return new PackageFeed(url, packages);
        }

        public static PackageFeed Create(IPackageFeed packageFeed)
        {
            return new PackageFeed(packageFeed);
        }
    }
}
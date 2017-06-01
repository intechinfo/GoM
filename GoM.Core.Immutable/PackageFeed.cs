using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class PackageFeed : IPackageFeed
    {
        PackageFeed(Uri url, ImmutableList<PackageInstance> packages)
        {
            Packages = packages ?? throw new ArgumentException(nameof(packages));
            Url = url ?? throw new ArgumentException(nameof(url));

            // Check duplicates
            if (CheckDuplicates(packages)) throw new ArgumentException($"Duplicates found in {nameof(packages)}");
        }

        PackageFeed(IPackageFeed packageFeed)
        {
            Url = packageFeed.Url ?? throw new ArgumentException(nameof(packageFeed.Url));
            Packages = packageFeed.Packages == null ? throw new ArgumentException(nameof(packageFeed.Packages)) : ImmutableList.Create(packageFeed.Packages.Select(x => PackageInstance.Create(x)).ToArray());


            // Check dulicates
            if (CheckDuplicates(Packages)) throw new ArgumentException($"Duplicates found in {nameof(Packages)}");
        }

        public Uri Url { get; }

        public ImmutableList<PackageInstance> Packages { get; } = ImmutableList.Create<PackageInstance>();

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;

        public static PackageFeed Create(Uri url, ImmutableList<PackageInstance> packages)
        {
            return new PackageFeed(url, packages);
        }

        public static PackageFeed Create(IPackageFeed packageFeed) => new PackageFeed(packageFeed);

        bool CheckDuplicates(ImmutableList<PackageInstance> packages)
        {
            return packages.Distinct(
                EqualityComparerGenerator.CreateEqualityComparer<PackageInstance>(
                    (x, y) => x.Name == y.Name && x.Version == y.Version,
                    x => GetPackageHashCode(x))
                ).Count() < packages.Count;
        }

        int GetPackageHashCode(PackageInstance package)
        {
            return 17 * (23 + package.Name.GetHashCode() + package.Version.GetHashCode());
        }
    }
}
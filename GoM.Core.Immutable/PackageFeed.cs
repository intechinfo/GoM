using System;
using System.Collections.Generic;

namespace GoM.Core.Immutable
{
    public class PackageFeed : IPackageFeed
    {
        Uri IPackageFeed.Url => throw new NotImplementedException();

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => throw new NotImplementedException();

        public static PackageFeed Create()
        {
            return new PackageFeed();
        }
    }
}
using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class PackageFeed : IPackageFeed
    {
        public Uri Url { get; }

        public PackageFeed(XElement e)
        {
            this.Url = new Uri(e.Attribute(GoMAttributeNamesV1.PACKAGE_FEED_URL).Value);
            this.Packages = new List<PackageInstance>();
            foreach (var t in e.Elements(GoMAttributeNamesV1.PACKAGE_INSTANCE))
            {
                this.Packages.Add(new PackageInstance(t));
            }
        }



        public List<PackageInstance> Packages { get; } 

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;

        public PackageFeed(Uri url)
        {
            Url = url;
            Packages = new List<PackageInstance>();
        }
    }
}
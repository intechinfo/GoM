using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class PackageFeed : IPackageFeed
    {

        public PackageFeed(XElement e)
        {
            this.Url = new Uri(e.Attribute(nameof(Uri)).Value);
            this.Packages = new List<PackageInstance>();
            foreach (var t in e.Elements(nameof(PackageInstance)))
            {
                this.Packages.Add(new PackageInstance(t));
            }
        }

        public Uri Url { get; set; }

        public List<PackageInstance> Packages { get; } = new List<PackageInstance>();

        IReadOnlyCollection<IPackageInstance> IPackageFeed.Packages => Packages;
    }
}
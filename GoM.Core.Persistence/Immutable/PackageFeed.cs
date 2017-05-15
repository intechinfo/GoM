using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class PackageFeed : IPackageFeed
    {
        private XElement el;
        public Uri Url { get; }

        public PackageFeed ( XElement el )
        {
            this.el = el;
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
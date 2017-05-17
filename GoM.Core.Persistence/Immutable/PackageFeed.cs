﻿using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace GoM.Core.Persistence
{
    public class PackageFeed : IPackageFeed
    {
        public Uri Url { get; }

        public PackageFeed(XElement e)
        {
            this.Url = new Uri(e.Attribute(nameof(Url)).Value);
            this.Packages = new List<PackageInstance>();
       //     this.Packages = e.Elements(nameof(IPackageInstance)).Select(t => new PackageInstance(t)).ToList();
            foreach (var t in e.Elements(nameof(IPackageInstance)))
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
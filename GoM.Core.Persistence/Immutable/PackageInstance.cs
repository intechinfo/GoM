using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class PackageInstance : IPackageInstance
    {
        public PackageInstance(XElement e)
        {
            this.Name = e.Attribute(nameof(Name)).Value;
            this.Version = e.Attribute(nameof(Version)).Value;
        }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
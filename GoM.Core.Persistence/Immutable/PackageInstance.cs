using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class PackageInstance : IPackageInstance
    {
        public string Name { get; }
        public PackageInstance(XElement e)
        {
            this.Name = e.Attribute(nameof(Name)).Value;
            this.Version = e.Attribute(nameof(Version)).Value;
        }


        public string Version { get; }


        public PackageInstance(string name, string version)
        {
            Name    = name;
            Version = Version;
        }
    }
}
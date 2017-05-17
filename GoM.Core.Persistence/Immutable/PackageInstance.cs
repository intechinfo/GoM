using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class PackageInstance : IPackageInstance
    {

        public const string PACKAGE_INSTANCE = "packageInstance";
        public const string PACKAGE_INSTANCE_VERSION = "version";
        public const string PACKAGE_INSTANCE_NAME = "name";

        public string Name { get; }
        public PackageInstance(XElement e)
        {
            this.Name = e.Attribute(PACKAGE_INSTANCE_NAME).Value;
            this.Version = e.Attribute(PACKAGE_INSTANCE_VERSION).Value;
        }


        public string Version { get; }


        public PackageInstance(string name, string version)
        {
            Name    = name;
            Version = Version;
        }
    }
}
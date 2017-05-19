using System;

namespace GoM.Core.Immutable
{
    public class PackageInstance : IPackageInstance
    {
        public string Name { get; }

        public string Version { get; }

        PackageInstance(string name, string version)
        {
            Name = name;
            Version = version;
        }

        PackageInstance(IPackageInstance packageInstance)
        {
            Name = packageInstance.Name;
            Version = packageInstance.Version;
        }

        public static PackageInstance Create(IPackageInstance packageInstance)
        {
            return new PackageInstance(packageInstance);
        }

        public static PackageInstance Create(string name, string version)
        {
            return new PackageInstance(name, version);
        }
    }
}
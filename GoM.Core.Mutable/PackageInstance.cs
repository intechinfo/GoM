using System;

namespace GoM.Core.Mutable
{
    public class PackageInstance : IPackageInstance
    {
        public PackageInstance()
        {
        }

        /// <summary>
        /// Creates a Mutable PackageInstance from an existing IPackageInstance, ie from an Immutable PackageInstance
        /// </summary>
        /// <param name="package"></param>
        public PackageInstance(IPackageInstance package)
        {
            Name = package.Name;
            Version = package.Version;
        }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
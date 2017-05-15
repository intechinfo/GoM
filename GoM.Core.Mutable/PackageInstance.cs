using System;

namespace GoM.Core.Mutable
{
    public class PackageInstance : IPackageInstance
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}
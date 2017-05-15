using GoM.Core;
using System;

namespace GoM.Core.Persistence
{
    public class PackageInstance : IPackageInstance
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}
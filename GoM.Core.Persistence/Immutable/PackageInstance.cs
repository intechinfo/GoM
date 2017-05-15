using GoM.Core;
using System;

namespace GoM.Persistence
{
    public class PackageInstance : IPackageInstance
    {
        public string Name { get; }

        public string Version { get; }


        public PackageInstance(string name, string version)
        {
            Name    = name;
            Version = Version;
        }
    }
}
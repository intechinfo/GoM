using System;

namespace GoM.Core.Mutable
{
    public class TargetDependency : ITargetDependency
    {
        public TargetDependency(string name, string version)
        {
            Name = name;
            Version = version;
        }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
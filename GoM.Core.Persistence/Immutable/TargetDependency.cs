using GoM.Core;
using System;

namespace GoM.Core.Persistence
{
    public class TargetDependency : ITargetDependency
    {
        public string Name { get; }

       
        public string Version { get; }


        public TargetDependency(string name, string version)
        {
            Name    = name;
            Version = version;
        }

    }
}
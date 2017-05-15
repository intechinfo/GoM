using GoM.Core;
using System;

namespace GoM.Persistence
{
    public class TargetDependency : ITargetDependency
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}
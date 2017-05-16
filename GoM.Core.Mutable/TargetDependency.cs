using System;

namespace GoM.Core.Mutable
{
    public class TargetDependency : ITargetDependency
    {
        public string Name { get; set; }

        public string Version { get; set; }
    }
}
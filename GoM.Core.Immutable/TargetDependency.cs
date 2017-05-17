using System;

namespace GoM.Core.Immutable
{
    public class TargetDependency : ITargetDependency
    {
        public string Name { get; }

        public string Version { get; }
    }
}
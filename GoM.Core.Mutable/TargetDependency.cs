using System;

namespace GoM.Core.Mutable
{
    public class TargetDependency : ITargetDependency
    {
        public TargetDependency()
        {
        }

        /// <summary>
        /// Creates a Mutable TargetDependency from an existing ITargetDependency, ie from an Immutable TargetDependency
        /// </summary>
        /// <param name="dep"></param>
        public TargetDependency(ITargetDependency dep)
        {
            Name = dep.Name;
            Version = dep.Version;
        }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
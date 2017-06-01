using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class Target : ITarget
    {
        public Target()
        {
        }

        /// <summary>
        /// Creates a Mutable Target from an existing ITarget, ie from an Immutable Target
        /// </summary>
        /// <param name="target"></param>
        public Target(ITarget target)
        {
            Name = target.Name;
            Dependencies = (List<TargetDependency>)target.Dependencies;
        }

        public string Name { get; set; }

        public List<TargetDependency> Dependencies { get; } = new List<TargetDependency>();

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;
    }
}
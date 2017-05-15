using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class Target : ITarget
    {
        public Target(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public List<TargetDependency> Dependencies { get; } = new List<TargetDependency>();

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;
    }
}
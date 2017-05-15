using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Persistence
{
    public class Target : ITarget
    {
        public string Name { get; }

        public List<TargetDependency> Dependencies { get; } 

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;

        public Target(string name)
        {
            Name = name;
            Dependencies = new List<TargetDependency>();
        }

    }
}
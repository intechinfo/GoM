using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GoM.Core.Immutable
{
    public class Target : ITarget
    {
        private ITarget target;

        public Target(ITarget target)
        {
            Name = target.Name ?? throw new ArgumentException(nameof(target.Name));
            if (target.Dependencies != null) Dependencies = (ImmutableList<TargetDependency>)target.Dependencies;
        }

        Target(string name, ImmutableList<TargetDependency> dependencies = null)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            if (dependencies != null) Dependencies = dependencies;
        }

        public string Name { get; }

        public ImmutableList<TargetDependency> Dependencies { get; } = ImmutableList.Create<TargetDependency>();

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;

        public static Target Create(ITarget target) => target as Target ?? new Target(target);
        public static Target Create(string name, ImmutableList<TargetDependency> dependencies = null) =>  new Target(name, dependencies);
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GoM.Core.Immutable
{
    public class Target : ITarget
    {
        public string Name { get; }

        public ImmutableList<TargetDependency> Dependencies { get; } = ImmutableList.Create<TargetDependency>();

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;
    }
}
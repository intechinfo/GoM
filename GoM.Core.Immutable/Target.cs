using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class Target : ITarget
    {
        private Target(ITarget target)
        {
            Name = target.Name ?? throw new ArgumentException(nameof(target.Name));
            if (target.Dependencies != null) Dependencies = (ImmutableList<TargetDependency>)target.Dependencies;

            // Check duplicates
            if (CheckDuplicates(Dependencies)) throw new ArgumentException($"Duplicates found in {nameof(Dependencies)}");
        }

        Target(string name, ImmutableList<TargetDependency> dependencies = null)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            if (dependencies != null) Dependencies = dependencies;

            // Check duplicates
            if (CheckDuplicates(Dependencies)) throw new ArgumentException($"Duplicates found in {nameof(Dependencies)}");
        }

        public string Name { get; }

        public ImmutableList<TargetDependency> Dependencies { get; } = ImmutableList.Create<TargetDependency>();

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;

        public static Target Create(ITarget target) => target as Target ?? new Target(target);
        public static Target Create(string name, ImmutableList<TargetDependency> dependencies = null) => new Target(name, dependencies);

        bool CheckDuplicates(ImmutableList<TargetDependency> dependencies)
        {
            return dependencies.Distinct(
                EqualityComparerGenerator.CreateEqualityComparer<TargetDependency>(
                    (x, y) => x.Name == y.Name && x.Version == y.Version, x => GetTargetHashCode(x)
                    )
                ).Count() < dependencies.Count;
        }

        int GetTargetHashCode(TargetDependency target)
        {
            return 17 * (target.Name.GetHashCode() + target.Version.GetHashCode());
        }
    }
}
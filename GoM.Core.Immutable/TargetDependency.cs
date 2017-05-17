using System;

namespace GoM.Core.Immutable
{
    public class TargetDependency : ITargetDependency
    {
        public string Name { get; }

        public string Version { get; }

        TargetDependency(string name, string version)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Version = version ?? throw new ArgumentException(nameof(version));
        }

        TargetDependency(ITargetDependency targetDependency)
        {
            Name = targetDependency.Name ?? throw new ArgumentException(nameof(targetDependency.Name));
            Version = targetDependency.Version ?? throw new ArgumentException(nameof(targetDependency.Version));
        }

        public static TargetDependency Create(string name, string version) => new TargetDependency(name, version);
        public static TargetDependency Create(ITargetDependency dep) => dep as TargetDependency ?? new TargetDependency(dep);
    }
}
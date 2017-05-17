using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class Project : IProject
    {
        private IProject details;

        public Project(IProject details)
        {
            Debug.Assert(!(details is Project));
            Path = details.Path ?? throw new ArgumentException(nameof(details.Path));
            if (details.Targets != null) Targets = (ImmutableList<Target>)details.Targets;
        }

        public Project(string path, ImmutableList<Target> targets =  null)
        {
            Path = path ?? throw new ArgumentException(nameof(path));
            if (targets != null) Targets = targets;
        }

        public ImmutableList<Target> Targets { get; } = ImmutableList.Create<Target>();

        public string Path { get; }

        public Project Details { get; }

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        IProject IBasicProject.Details => Details;

        internal static Project Create(IProject details) => details as Project ?? new Project(details);

        internal static Project Create(string path, ImmutableList<Target> targets = null) => new Project(path, targets);

    }
}
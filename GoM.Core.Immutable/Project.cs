using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace GoM.Core.Immutable
{
    public class Project : IProject
    {
        public ImmutableList<Target> Targets { get; } = ImmutableList.Create<Target>();

        public string Path { get; }

        public Project Details { get; }

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        IProject IBasicProject.Details => Details;
    }
}
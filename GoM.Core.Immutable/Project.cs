﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class Project : IProject
    {
        private Project(IProject details)
        {
            Debug.Assert(!(details is Project));
            Path = details.Path ?? throw new ArgumentException(nameof(details.Path));
            Targets = details.Targets == null ? ImmutableList.Create<Target>() : ImmutableList.Create(details.Targets.Select(x => Target.Create(x)).ToArray());

            // Check duplicates
            if (CheckDuplicates(Targets)) throw new ArgumentException($"A duplicate target was found in {nameof(details.Targets)}");
        }

        private Project(string path, ImmutableList<Target> targets =  null)
        {
            Path = path ?? throw new ArgumentException(nameof(path));
            if (targets != null) Targets = targets;

            // Check duplicates
            if (CheckDuplicates(Targets)) throw new ArgumentException($"A duplicate target was found in {nameof(Targets)}");
        }

        public ImmutableList<Target> Targets { get; } = ImmutableList.Create<Target>();

        public string Path { get; }

        public Project Details => this;

        bool CheckDuplicates(ImmutableList<Target> targets)
        {
            return targets.Distinct(
                EqualityComparerGenerator.CreateEqualityComparer<Target>(
                    (x, y) => x.Name == y.Name, x => x.Name.GetHashCode()
                    )
                ).Count() < targets.Count;
        }

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        IProject IBasicProject.Details => Details;

        public static Project Create(IProject details) => details as Project ?? new Project(details);

        public static Project Create(string path, ImmutableList<Target> targets = null) => new Project(path, targets);

    }
}
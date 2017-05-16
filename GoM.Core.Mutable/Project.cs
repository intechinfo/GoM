using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class Project : IProject
    {
        public string Path { get; set; }

        public List<Dependency> Targets { get; } = new List<Dependency>();

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;
    }
}
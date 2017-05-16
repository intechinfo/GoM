using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class Project : IProject
    {
        public string Path { get; set; }

        public List<Target> Targets { get; } = new List<Target>();

        public Project Details => this;

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        IProject IBasicProject.Details => Details;
    }
}
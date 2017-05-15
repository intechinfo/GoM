using GoM.Core;
using System;
using System.Collections.Generic;

namespace GoM.Core.Persistence
{
    public class Project : IProject
    {
        public string Path { get; }

        public List<Target> Targets { get; } 

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        public Project(string path)
        {
            Path    = path;
            Targets = new List<Target>();
        }
    }
}
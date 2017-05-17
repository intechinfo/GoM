using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class Project : IProject
    {

        public Project()
        {
        }

        /// <summary>
        /// Creates a Mutable Project from an existing IProject, ie from an Immutable Project
        /// </summary>
        /// <param name="project"></param>
        public Project(IProject project)
        {
            Path = project.Path;
            Targets = (List<Target>)project.Targets;
        }

        public string Path { get; set; }

        public List<Target> Targets { get; } = new List<Target>();

        public Project Details => this;

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        IProject IBasicProject.Details => Details;
    }
}
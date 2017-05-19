using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class BasicProject : IBasicProject
    {
        public BasicProject()
        {
        }

        /// <summary>
        /// Creates a Mutable BasicProject from an existing IBasicProject, ie from an Immutable BasicProject
        /// </summary>
        /// <param name="project"></param>
        public BasicProject(IBasicProject project)
        {
            Path = project.Path;
            Details = (Project)project.Details;
        }

        public string Path { get; set; }

        public Project Details { get; set; }

        IProject IBasicProject.Details => Details;
        
    }
}
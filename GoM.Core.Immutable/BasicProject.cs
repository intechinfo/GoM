using System;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class BasicProject : IBasicProject
    {
        private BasicProject(IBasicProject project)
        {
            Debug.Assert(!(project is BasicProject));
            Path = project.Path ?? throw new ArgumentException(nameof(project.Path));
            Details = project.Details ==  null ? null : Project.Create(project.Details);
        }

        BasicProject(string path, Project details = null)
        {
            Path = path ?? throw new ArgumentException(nameof(path));
            Details = details;
        }

        public string Path { get; }

        public Project Details { get; }

        IProject IBasicProject.Details => Details;

        public static BasicProject Create(string path, Project details = null) => new BasicProject(path, details);
        public static BasicProject Create(IBasicProject project) => project as BasicProject ?? new BasicProject(project);

    }
}
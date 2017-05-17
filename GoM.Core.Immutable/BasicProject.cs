using System;

namespace GoM.Core.Immutable
{
    public class BasicProject : IBasicProject
    {
        public string Path { get; }

        public Project Details { get; }

        IProject IBasicProject.Details => Details;
    }
}
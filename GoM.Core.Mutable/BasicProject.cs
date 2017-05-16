using System;
using System.Collections.Generic;

namespace GoM.Core.Mutable
{
    public class BasicProject : IBasicProject
    {
        public string Path { get; set; }

        public Project Details { get; set; }

        IProject IBasicProject.Details => Details;
    }
}
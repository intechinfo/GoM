using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.FSAnalyzer
{
    public class ProjectTypeAttribute : Attribute
    {
        public string Name { get; }
        public ProjectTypeAttribute(string name)
        {
            this.Name = name;
        }
    }
}

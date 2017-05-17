using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class TargetDependency : ITargetDependency
    {
        private XElement t;
        public string Name { get; }

        public TargetDependency ( XElement t )
        {
            this.t = t;
            Name = t.Attribute( GoMAttributeNamesV1.TARGET_DEPENDENCY_NAME ).Value;
            Version = t.Attribute( GoMAttributeNamesV1.TARGET_DEPENDENCY_VERSION ).Value;
        }

       
        public string Version { get; }


        public TargetDependency(string name, string version)
        {
            Name    = name;
            Version = version;
        }

    }
}
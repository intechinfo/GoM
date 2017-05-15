using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class TargetDependency : ITargetDependency
    {
        private XElement t;

        public TargetDependency ( XElement t )
        {
            this.t = t;
            Name = t.Attribute( nameof( Name ) ).Value;
            Version = t.Attribute( nameof( Version ) ).Value;
        }

        public string Name { get; set; }

        public string Version { get; set; }
    }
}
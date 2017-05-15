using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class Target : ITarget
    {
        private XElement t;

        public Target ( XElement node )
        {
            this.t = node;
            Name = node.Attribute( nameof( Name ) ).Value;
            Dependencies = new List<TargetDependency>();
            foreach(var t in node.Elements(nameof(Dependencies)))
            {
                Dependencies.Add( new TargetDependency( t ) );
            }

        }

        public string Name { get; set; }

        public List<TargetDependency> Dependencies { get; } = new List<TargetDependency>();

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;

        public static implicit operator List<object>( Target v )
        {
            throw new NotImplementedException();
        }
    }
}
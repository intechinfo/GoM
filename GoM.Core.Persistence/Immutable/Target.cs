using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class Target : ITarget
    {
        private XElement t;
        public string Name { get; }

        public Target ( XElement node )
        {
            this.t = node;
            Name = node.Attribute( nameof( Name ) ).Value;
            Dependencies = new List<TargetDependency>();
            foreach(var t in node.Elements(nameof(ITargetDependency)))
            {
                Dependencies.Add( new TargetDependency( t ) );
            }

        }


        public List<TargetDependency> Dependencies { get; } 

        IReadOnlyCollection<ITargetDependency> ITarget.Dependencies => Dependencies;

        public static implicit operator List<object>( Target v )
        {
            throw new NotImplementedException();
        }
        public Target(string name)
        {
            Name = name;
            Dependencies = new List<TargetDependency>();
        }

    }
}
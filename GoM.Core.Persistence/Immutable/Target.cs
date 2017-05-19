using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace GoM.Core.Persistence
{
    public class Target : ITarget
    {


        public const string TARGET = "target";
        public const string TARGET_NAME = "name";


        private XElement t;
        public string Name { get; }

        public Target ( XElement node )
        {
            this.t = node;
            Name = node.Attribute( TARGET_NAME ).Value;
            Dependencies = new List<TargetDependency>();
            Dependencies = node.Elements(TargetDependency.TARGET_DEPENDENCY).Select(t => new TargetDependency(t)).ToList();
            //foreach (var t in node.Elements(TargetDependency.TARGET_DEPENDENCY))
            //{
            //    Dependencies.Add( new TargetDependency( t ) );
            //}

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
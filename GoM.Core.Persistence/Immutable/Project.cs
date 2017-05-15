using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class Project : IProject
    {
        private XElement t;

        public Project ( XElement node )
        {
            this.t = node;
            Path = node.Attribute( nameof( Path ) ).Value;

            Targets = new List<Target>();
            foreach ( var t in node.Elements(nameof(Targets)))
            {
                Targets = new Target(t);
            }
        }

        public string Path { get; set; }

        public List<Target> Targets { get; } = new List<Target>();

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;
    }
}
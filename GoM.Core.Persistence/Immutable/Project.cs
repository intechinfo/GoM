using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace GoM.Core.Persistence
{
    public class Project : IProject
    {
        public const string PROJECT = "project";
        public const string PROJECT_PATH = "path";


        private XElement t;
        public string Path { get; }

        public Project ( XElement node )
        {
            this.t = node;
            Path = node.Attribute( PROJECT_PATH).Value;

            Targets = new List<Target>();
            Targets = node.Elements(Target.TARGET).Select(t => new Target(t)).ToList();
            //foreach ( var t in node.Elements( Target.TARGET ))
            //{
            //    Targets.Add(new Target(t));
            //}
        }

        public List<Target> Targets { get; } 

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        public IProject Details => this;

        public Project(string path)
        {
            Path    = path;
            Targets = new List<Target>();
        }



    }
}
﻿using GoM.Core;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class Project : IProject
    {
        private XElement t;
        public string Path { get; }

        public Project ( XElement node )
        {
            this.t = node;
            Path = node.Attribute( GoMAttributeNamesV1.PROJECT_PATH).Value;

            Targets = new List<Target>();
            foreach ( var t in node.Elements( GoMAttributeNamesV1.TARGET))
            {
                Targets.Add(new Target(t));
            }
        }

        public List<Target> Targets { get; } 

        IReadOnlyCollection<ITarget> IProject.Targets => Targets;

        public IProject Details => throw new NotImplementedException();

        public Project(string path)
        {
            Path    = path;
            Targets = new List<Target>();
        }



    }
}
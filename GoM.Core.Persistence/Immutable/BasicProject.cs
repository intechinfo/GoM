﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicProject : IBasicProject
    {

        public BasicProject ( XElement t )
        {
            Path = t.Attribute( nameof(Path) ).Value;

            var node = t.Element( typeof(IProject).Name );
            Details = node != null ? new Project( node ) : null;
        }

        public string Path { get; }

        public IProject Details { get; }
    }
}

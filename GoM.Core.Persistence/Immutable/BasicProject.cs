using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BasicProject : IBasicProject
    {
        public const string BASIC_PROJECT = "basicproject";
        public const string BASIC_PROJECT_PATH = "path";
        public const string BASIC_PROJECT_DETAILS = "details";

        public BasicProject ( XElement t )
        {
            Path = t.Attribute( BASIC_PROJECT_PATH ).Value;

            var node = t.Element( Project.PROJECT );
            Details = node != null ? new Project( node ) : null;
        }

        public string Path { get; }

        public IProject Details { get; }
    }
}

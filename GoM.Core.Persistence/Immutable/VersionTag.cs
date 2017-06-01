using GoM.Core; using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class VersionTag : IVersionTag
    {
        public string FullName { get; }

        public VersionTag(XElement e)
        {
            FullName = e.Attribute( nameof( FullName ) ).Value;
        }

        public VersionTag(string fullName)
        {
            FullName = fullName;
        }

    }
}
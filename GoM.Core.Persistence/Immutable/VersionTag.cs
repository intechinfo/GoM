using GoM.Core; using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class VersionTag : IVersionTag
    {
        public string FullName { get; }

        public VersionTag(XElement e)
        {
            FullName = e.Attribute( GoMAttributeNamesV1.VERSION_TAG_FULL_NAME ).Value;
        }

        public VersionTag(string fullName)
        {
            FullName = fullName;
        }

    }
}
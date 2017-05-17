using GoM.Core; using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class VersionTag : IVersionTag
    {
        public const string VERSION_TAG = "versionTag";
        public const string VERSION_TAG_FULL_NAME = "fullName";


        public string FullName { get; }

        public VersionTag(XElement e)
        {
            FullName = e.Attribute( VERSION_TAG_FULL_NAME ).Value;
        }

        public VersionTag(string fullName)
        {
            FullName = fullName;
        }

    }
}
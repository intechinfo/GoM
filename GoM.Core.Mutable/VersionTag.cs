using System;

namespace GoM.Core.Mutable
{
    public class VersionTag : IVersionTag
    {
        public VersionTag()
        {
        }

        public VersionTag(IVersionTag tag)
        {
            FullName = tag.FullName;
        }

        public string FullName { get; set; }
    }
}
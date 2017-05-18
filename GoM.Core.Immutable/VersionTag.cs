using System;

namespace GoM.Core.Immutable
{
    public class VersionTag : IVersionTag
    {
        VersionTag(string fullName)
        {
            FullName = fullName ?? throw new ArgumentException(nameof(fullName));
        }

        VersionTag(IVersionTag lastTag)
        {
            FullName = lastTag.FullName ?? throw new ArgumentException(nameof(lastTag.FullName));
        }

        public string FullName { get; }

        public static VersionTag Create(IVersionTag lastTag) => lastTag as VersionTag ?? new VersionTag(lastTag);

        public static VersionTag Create(string name) => new VersionTag(name);
    }
}
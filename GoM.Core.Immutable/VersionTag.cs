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

        internal static VersionTag Create(IVersionTag lastTag) => lastTag as VersionTag ?? new VersionTag(lastTag);

        internal static VersionTag Create(string name) => new VersionTag(name);
    }
}
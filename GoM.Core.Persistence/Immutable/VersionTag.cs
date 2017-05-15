using GoM.Core; using System;


namespace GoM.Persistence
{
    public class VersionTag : IVersionTag
    {
        public string FullName { get; }

        public VersionTag(string fullName)
        {
            FullName = fullName;
        }

    }
}
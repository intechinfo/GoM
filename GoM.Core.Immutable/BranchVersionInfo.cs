using System;

namespace GoM.Core.Immutable
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        public VersionTag LastTag { get; }

        public int LastTagDepth { get; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;
    }
}
using GoM.Core; using System;

namespace GoM.Core.Persistence
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        public VersionTag LastTag { get; }

        public int LastTagDepth { get; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;

        public BranchVersionInfo(VersionTag lastTag, int lastTagDepth)
        {
            LastTag = lastTag;
            LastTagDepth = lastTagDepth;
        }

    }
}
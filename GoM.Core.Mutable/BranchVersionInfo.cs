using System;

namespace GoM.Core.Mutable
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        public VersionTag LastTag { get; set; }

        public int LastTagDepth { get; set; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;
    }
}
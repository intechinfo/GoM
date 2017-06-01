using System;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        BranchVersionInfo(int lastTagDepth, VersionTag lastTag)
        {
            LastTagDepth = lastTagDepth;
            LastTag = lastTag ?? throw new ArgumentException(nameof(lastTag));
        }

        BranchVersionInfo(IBranchVersionInfo version)
        {
            Debug.Assert(!(version is BranchVersionInfo));
            LastTag = version.LastTag != null ? VersionTag.Create(version.LastTag) : null;
            LastTagDepth = version.LastTagDepth;
        }

        public VersionTag LastTag { get; }

        public int LastTagDepth { get; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;

        public static BranchVersionInfo Create(IBranchVersionInfo version) => version as BranchVersionInfo ?? new BranchVersionInfo(version);

        public static BranchVersionInfo Create(int lastTagDepth, VersionTag lastTag)
        {
            return new BranchVersionInfo(lastTagDepth, lastTag);
        }
    }
}
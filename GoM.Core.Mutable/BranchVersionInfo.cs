using System;

namespace GoM.Core.Mutable
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        public BranchVersionInfo()
        {
        }

        /// <summary>
        /// Creates a Mutable BranchVersionInfo from an existing IBranchVersionInfo, ie from an Immutable BranchVersionInfo
        /// </summary>
        /// <param name="version"></param>
        public BranchVersionInfo(IBranchVersionInfo version)
        {
            LastTag = version is BranchVersionInfo ? (VersionTag)version.LastTag : new VersionTag(version.LastTag);
            LastTagDepth = version.LastTagDepth;
        }
        public VersionTag LastTag { get; set; }

        public int LastTagDepth { get; set; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;
    }
}
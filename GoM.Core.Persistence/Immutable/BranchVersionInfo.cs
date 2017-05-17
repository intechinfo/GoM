using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        public const string BRANCH_VERSION_INFO = "branchVersionInfo";
        public const string BRANCH_VERSION_INFO_LAST_TAG_DEPTH = "LastTagDepth";

        private XElement xElement;
        public VersionTag LastTag { get; }

        public BranchVersionInfo ( XElement xElement )
        {
            this.xElement = xElement;
            LastTag = new VersionTag(xElement.Element( VersionTag.VERSION_TAG ));
            LastTagDepth = int.Parse(xElement.Attribute( BRANCH_VERSION_INFO_LAST_TAG_DEPTH ).Value);
        }


        public int LastTagDepth { get; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;

        public BranchVersionInfo(VersionTag lastTag, int lastTagDepth)
        {
            LastTag = lastTag;
            LastTagDepth = lastTagDepth;
        }

    }
}
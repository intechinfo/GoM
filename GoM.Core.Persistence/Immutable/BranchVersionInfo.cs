using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        private XElement xElement;
        public VersionTag LastTag { get; }

        public BranchVersionInfo ( XElement xElement )
        {
            this.xElement = xElement;
            LastTag = new VersionTag(xElement.Element( typeof( IVersionTag ).Name ));
            LastTagDepth = int.Parse(xElement.Attribute( nameof( LastTagDepth ) ).Value);
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
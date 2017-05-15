using GoM.Core;
using System;
using System.Xml.Linq;

namespace GoM.Core.Persistence
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        private XElement xElement;

        public BranchVersionInfo ( XElement xElement )
        {
            this.xElement = xElement;
        }

        public VersionTag LastTag { get; set; }

        public int LastTagDepth { get; set; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;
    }
}
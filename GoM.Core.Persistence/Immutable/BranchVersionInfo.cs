﻿using GoM.Core; using System;

namespace GoM.Persistence
{
    public class BranchVersionInfo : IBranchVersionInfo
    {
        public VersionTag LastTag { get; set; }

        public int LastTagDepth { get; set; }

        IVersionTag IBranchVersionInfo.LastTag => LastTag;
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core
{
    public interface IBranchVersionInfo
    {
        IVersionTag LastTag { get; }

        int LastTagDepth { get; }
    }
}

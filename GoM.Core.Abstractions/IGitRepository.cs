using System;
using System.Collections.Generic;

namespace GoM.Core
{
    public interface IGitRepository : IBasicGitRepository
    {
        Uri Url { get; }

        IReadOnlyCollection<IBasicGitBranch> Branches { get; }
    }
}
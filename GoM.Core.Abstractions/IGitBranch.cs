using System.Collections.Generic;

namespace GoM.Core
{
    public interface IGitBranch : IBasicGitBranch
    {
        IReadOnlyCollection<IBasicProject> Projects { get; }

        IBranchVersionInfo Version { get; }
    }
}
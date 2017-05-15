using System.Collections.Generic;

namespace GoM.Core
{
    public interface IGitBranch : IBasicGitBranch
    {
        IReadOnlyCollection<IProject> Projects { get; }

        IBranchVersionInfo Version { get; }
    }
}
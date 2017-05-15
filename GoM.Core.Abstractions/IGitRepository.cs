using System.Collections.Generic;

namespace GoM.Core
{
    public interface IGitRepository : IBasicGitRepository
    {
        IReadOnlyCollection<IBasicGitBranch> Branches { get; }
    }
}
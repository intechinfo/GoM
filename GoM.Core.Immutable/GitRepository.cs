using System;
using System.Collections.Generic;

namespace GoM.Core.Immutable
{
    public class GitRepository : IGitRepository
    {
        IReadOnlyCollection<IBasicGitBranch> IGitRepository.Branches => throw new NotImplementedException();

        string IBasicGitRepository.Path => throw new NotImplementedException();

        Uri IBasicGitRepository.Url => throw new NotImplementedException();

        IGitRepository IBasicGitRepository.Details => throw new NotImplementedException();
    }
}
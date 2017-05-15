using System;

namespace GoM.Core.Immutable
{
    public class BasicGitRepository : IBasicGitRepository
    {
        string IBasicGitRepository.Path => throw new NotImplementedException();

        Uri IBasicGitRepository.Url => throw new NotImplementedException();

        IGitRepository IBasicGitRepository.Details => throw new NotImplementedException();
    }
}
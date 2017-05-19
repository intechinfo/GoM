using System;

namespace GoM.Core
{
    public interface IBasicGitRepository
    {
        string Path { get; }

        IGitRepository Details { get; }
    }
}
using Microsoft.Extensions.FileProviders;
using System;

namespace GoM.Core
{
    public interface IBasicGitRepository
    {
        string Path { get; }

        Uri Url { get; }

        IGitRepository Details { get; }
    }
}
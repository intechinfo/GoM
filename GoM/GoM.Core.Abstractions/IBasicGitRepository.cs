using System;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core
{
    public interface IBasicGitRepository
    {
        IFileInfo Path { get; }

        Uri Url { get; }

        IGitRepository IgitRepository { get; }

    }
}
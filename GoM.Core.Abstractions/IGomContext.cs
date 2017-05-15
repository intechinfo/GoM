using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core
{
    public interface IGomContext
    {
        string RootPath { get; }

        IReadOnlyCollection<IBasicGitRepository> Repositories { get; }

        IReadOnlyCollection<IPackageFeed> Feeds { get; }
    }
}

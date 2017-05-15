using System;
using System.Collections.Generic;
using System.Text;


namespace GoM.Core
{
    public interface IGomContext
    {
        string RootPath { get; }

        IReadOnlyCollection<IBasicGitRepository> Repositories { get; }

        IReadOnlyCollection<IPackageFeed> Feeds { get; }
    }
}

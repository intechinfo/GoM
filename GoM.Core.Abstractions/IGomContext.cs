using System.Collections.Generic;

namespace GoM.Core
{
    public interface IGomContext
    {
        string RootPath { get; }

        IReadOnlyCollection<IBasicGitRepository> Repositories { get; }

        IReadOnlyCollection<IPackageFeed> Feeds { get; }
    }
}

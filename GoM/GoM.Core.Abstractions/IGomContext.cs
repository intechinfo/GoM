using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core
{
    public interface IGomContext
    {
        IFileInfo Root { get; }

        IReadOnlyCollection<IGitRepository> Repositories { get; }
    }
}

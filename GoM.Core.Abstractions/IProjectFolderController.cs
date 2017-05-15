using System;
using System.Collections.Generic;

namespace GoM.Core.Abstractions
{
    public interface IProjectFolderController
    {
        IReadOnlyCollection<IProject> Analyze(string path);
    }
}

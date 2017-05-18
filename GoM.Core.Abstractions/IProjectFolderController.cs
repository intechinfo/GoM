using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.Abstractions
{
    public interface IProjectFolderController
    {
        IReadOnlyCollection<IProject> Analyze(IFileProvider rootPath);
    }
}

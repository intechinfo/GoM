using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core
{
    public interface IProject
    {
        string Path { get; }

        IReadOnlyCollection<ITarget> Targets { get; }
    }
}

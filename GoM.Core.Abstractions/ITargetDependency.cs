using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core
{
    public interface ITargetDependency
    {
        string Name { get; }

        string Version { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core
{
    public interface ITarget
    {
        string Name { get; }

        IReadOnlyCollection<ITargetDependency> Dependencies { get; }
    }
}

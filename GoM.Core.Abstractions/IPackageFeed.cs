using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core
{
    public interface IPackageFeed
    {
        Uri Url { get; }

        IReadOnlyCollection<IPackageInstance> Packages { get; }
    }
}

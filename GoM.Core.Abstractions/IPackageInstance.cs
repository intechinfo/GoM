using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core
{
    public interface IPackageInstance
    {
        string Name { get; }

        string Version { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Abstractions
{
    public interface IProjectConfigReader
    {
        IEnumerable<ITarget> Targets { get; }
        void Read();
    }
}

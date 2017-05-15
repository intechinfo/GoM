using System.Collections.Generic;

namespace GoM.Core
{
    public interface IProject
    {
        string Path { get; }

        IReadOnlyCollection<ITarget> Targets { get; }
    }
}

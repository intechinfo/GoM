using System.Collections.Generic;

namespace GoM.Core
{
    public interface IProject : IBasicProject
    {
        IReadOnlyCollection<ITarget> Targets { get; }
    }
}

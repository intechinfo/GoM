using System.Collections.Generic;

namespace GoM.Core
{
    public interface IBasicProject
    {
        string Path { get; }

        IProject Details { get; }
    }
}

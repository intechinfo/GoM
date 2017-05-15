using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedReader
    { 
        IEnumerable<IPackageInstance> GetAllVersions(string name);

        IEnumerable<ITarget> GetDependencies(string name, string version);
       
    }
}

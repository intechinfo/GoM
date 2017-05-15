using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedReader
    { 
        IPackageInstance GetLatestVersion(IPackageInstance package);
        IPackageInstance GetSpecificVersion(IPackageInstance package);

        List<IPackageInstance> GetDependencies(IPackageInstance package);

        IPackageInstance Search(string packageName);
    }
}

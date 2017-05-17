using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedReader
    {
        Task<IEnumerable<IPackageInstance>> GetAllVersions(string name);
        Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name,string version);
        Task<IEnumerable<ITarget>> GetDependencies(string name, string version);
        Task<bool> FeedMatch(Uri adress);
    }
}

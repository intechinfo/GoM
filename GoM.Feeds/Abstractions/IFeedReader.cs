using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Threading.Tasks;
using GoM.Feeds.Results;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedReader: IDisposable
    {
        /// <summary>
        /// Gets all the version of a <see cref="IPackageInstance"/>
        /// </summary>
        /// <param name="name">the name of the package to search</param>
        /// <returns>A <see cref="Task"/> of all the instances getting retreived from the web</returns>
        Task<ReadPackagesResult> GetAllVersions(string name);
        /// <summary>
        /// Gets the newest version of a <see cref="IPackageInstance"/>
        /// </summary>
        /// <param name="name">the name of the package to search</param>
        /// <returns>A <see cref="Task"/> of all the instances getting retreived from the web</returns>
        Task<ReadPackagesResult> GetNewestVersions(string name,string version);
        /// <summary>
        /// Gets all the dependencies of a package for  given version and package name
        /// </summary>
        /// <param name="name">the name of the package to search</param>
        /// <param name="version">the version of the package to search</param>
        /// <returns>A <see cref="Task"/> of all the instances getting retreived from the web</returns>
        Task<ReadDependenciesResult> GetDependencies(string name, string version);
        /// <summary>
        /// Finds if a Feed is mathing a given endpoint
        /// </summary>
        /// <param name="adress">The endpoints <see cref="Uri"/> to check the result</param>
        /// <returns> the pending <see cref="Task"/> of the return</returns>
        Task<FeedMatchResult> FeedMatch(Uri adress);
    }
}

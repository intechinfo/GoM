using System;
using System.Collections.Generic;
using System.Text;
using GoM.Core;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedReader
    {
        /// <summary>
        /// Gets all the version of a <see cref="IPackageInstance"/>
        /// </summary>
        /// <param name="name">the name of the package to search</param>
        /// <returns>A <see cref="Task"/> of all the instances getting retreived from the web</returns>
        Task<IEnumerable<IPackageInstance>> GetAllVersions(string name);
        /// <summary>
        /// Gets the newest version of a <see cref="IPackageInstance"/>
        /// </summary>
        /// <param name="name">the name of the package to search</param>
        /// <returns>A <see cref="Task"/> of all the instances getting retreived from the web</returns>
        Task<IEnumerable<IPackageInstance>> GetNewestVersions(string name,string version);
        /// <summary>
        /// Gets all the dependencies of a package for  given version and package name
        /// </summary>
        /// <param name="name">the name of the package to search</param>
        /// <param name="version">the version of the package to search</param>
        /// <returns>A <see cref="Task"/> of all the instances getting retreived from the web</returns>
        Task<IEnumerable<ITarget>> GetDependencies(string name, string version);
        /// <summary>
        /// Finds if a Feed is mathing a given endpoint
        /// </summary>
        /// <param name="adress">The endpoints <see cref="Uri"/> to check the result</param>
        /// <returns> the pending <see cref="Task"/> of the return</returns>
        Task<bool> FeedMatch(Uri adress);
    }
}

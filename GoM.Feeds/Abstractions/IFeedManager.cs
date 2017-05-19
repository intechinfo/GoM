using GoM.Core;
using GoM.Feeds.Results;
using System;
using System.Collections.Generic;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedManager: IDisposable
    {
        /// <summary>
        /// Searches on all given <see cref="Uri"/> the matching <see cref="IFeedReader"/> for <see cref="IPackageInstance"/> new version
        /// </summary>
        /// <param name="packageFeeds">A <see cref="IEnumerable{Uri}"/> to find repositories about</param>
        /// <param name="packages">The list of <see cref="IPackageInstance"/> to get updates about</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/>having the searched package as key, all the new versions as vale</returns>
        GetPackagesResult GetNewestVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages);
        /// <summary>
        /// Searches on all given <see cref="Uri"/> the matching <see cref="IFeedReader"/> for <see cref="IPackageInstance"/> versions
        /// </summary>
        /// <param name="packageFeeds">A <see cref="IEnumerable{Uri}"/> to find repositories about</param>
        /// <param name="packages">The list of <see cref="IPackageInstance"/> to get updates about</param>
        /// <returns>A <see cref="IDictionary{TKey, TValue}"/>having the searched package as key, all the versions as vale</returns>
        GetPackagesResult GetAllVersions(IEnumerable<Uri> packageFeeds, IEnumerable<IPackageInstance> packages);
    }
}
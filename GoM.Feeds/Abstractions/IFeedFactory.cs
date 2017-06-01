using GoM.Core;
using GoM.Feeds.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedFactory: IDisposable
    {
        /// <summary>
        /// Returns all the <see cref="IFeedReader"/> from the inner factories
        /// </summary>
        IEnumerable<IFeedReader> FeedReaders { get; }
        /// <summary>
        /// Returns the search result <see cref="IFeedReader"/> that match at least one of the given <see cref="Uri"/>
        /// </summary>
        /// <param name="links">A list of <see cref="Uri"/> used as <see cref="IFeedReader"/> identifiers </param>
        /// <returns>A <see cref="IEnumerable{IFeedReader}"/></returns>
        Task<IEnumerable<FeedMatchResult>> Snif(IEnumerable<Uri> links);
        /// <summary>
        /// Returns the search result <see cref="IFeedReader"/> that match the given <see cref="Uri"/>
        /// </summary>
        /// <param name="link">A <see cref="Uri"/> used as <see cref="IFeedReader"/> identifier </param>
        /// <returns>A <see cref="IEnumerable{IFeedReader}"/></returns>
        Task<IEnumerable<FeedMatchResult>> Snif(Uri link);
    }
}
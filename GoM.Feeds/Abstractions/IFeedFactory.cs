using GoM.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoM.Feeds.Abstractions
{
    public interface IFeedFactory
    {
        /// <summary>
        /// Returns all the readers from the inner factories
        /// </summary>
        IEnumerable<IFeedReader> FeedReaders { get; }
        /// <summary>
        /// Returns the <see cref="IFeedReader"/> that match at least one of the given <see cref="Uri"/>
        /// </summary>
        /// <param name="links">A list of <see cref="Uri"/> used as <see cref="IFeedReader"/> identifiers </param>
        /// <returns>A <see cref="IEnumerable{IFeedReader}"/></returns>
        IEnumerable<IFeedReader> Snif(IEnumerable<Uri> links);
        /// <summary>
        /// Returns the <see cref="IFeedReader"/> that match the given <see cref="Uri"/>
        /// </summary>
        /// <param name="link">A <see cref="Uri"/> used as <see cref="IFeedReader"/> identifier </param>
        /// <returns>A <see cref="IEnumerable{IFeedReader}"/></returns>
        IEnumerable<IFeedReader> Snif(Uri link);
    }
}
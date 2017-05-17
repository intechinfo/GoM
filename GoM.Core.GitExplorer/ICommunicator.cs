using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using GoM.Core.Mutable;
namespace GoM.Core.GitExplorer
{
    interface ICommunicator
    {
        /// <summary>
        /// origin Url or Local path of repository.
        /// </summary>
        string Source { get; }
        /// <summary>
        /// Folder where repositories are stored by GoM.
        /// </summary>
        string ReposPath { get; }
        /// <summary>
        /// Repository instance of source.
        /// </summary>
        Repository Repository { get; set; }
        /// <summary>
        /// Path to the repository location.
        /// </summary>
        string Path { get; set; }
         Uri Url { get; }

        /// <summary>
        /// Load Repository instance of source.
        /// </summary>
        /// <returns>Repository</returns>
        Repository loadRepository();

        /// <summary>
        /// Get all files in repository.
        /// </summary>
        /// <param name="searchPattern">Model of search</param>
        /// <returns>All files</returns>
        List<string> getFiles(string searchPattern = "*");

        /// <summary>
        /// Get all Folders in repository.
        /// </summary>
        /// <param name="searchPattern">Model of search</param>
        /// <returns>All folders</returns>
        List<string> getFolders(string searchPattern = "*");

        /// <summary>
        /// Get BasicGitRepository instance of repository
        /// </summary>
        /// <returns>BasicGitRepository</returns>
        BasicGitRepository getBasicGitRepository();

        /// <summary>
        /// Get All Branches of repository
        /// </summary>
        /// <returns>List<BasicGitBranch></returns>
        List<BasicGitBranch> getAllBranches();

        /// <summary>
        /// Get a ExtensionStatictic Dictionary.
        /// </summary>
        /// <returns>Dictionary<String, ExtensionStatistic></returns>
        Dictionary<String, ExtensionFileStatistic> getExtensionDictionary();
    }
}

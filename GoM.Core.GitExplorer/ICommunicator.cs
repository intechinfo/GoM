using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;

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
        /// Path to the repository location.
        /// </summary>
        string Path { get; set; }
        /// <summary>
        /// Uri instance of source if repository is founded on internet.
        /// </summary>
        Uri Url { get; }

        /// <summary>
        /// Load Repository instance of source.
        /// </summary>
        /// <returns>Repository</returns>
        Repository loadRepository(string source);

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
        /// <returns>IDirectoryContents</returns>
        IDirectoryContents directoryContents();

        /// <summary>
        /// Get All Branches name
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        IEnumerable<string> getAllBranchesName();

        /// <summary>
        /// Get All Branches of a repository
        /// </summary>
        /// <returns>IEnumerable<string></returns>
        List<BasicGitBranch> getAllBranches();

        void Fetch();

    }
}

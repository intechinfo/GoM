using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using GoM.Core.Mutable;

/*
 *  A faire : 
 *  + Déterminer le type de projet (application c# windows, android, ios, web, etc...)
 *  + Déterminer les dépendances utilisées
 *  ...
 * 
 */

namespace GoM.Core.GitExplorer
{

    public struct ExtensionFileStatistic
    {
        public String extension;
        public int count;
        public List<String> listPath;
    }

    public class Communicator : ICommunicator
    {

        const string REPOS_DIRECTORY = "repos";

        /// <summary>
        /// origin Url or Local path of repository.
        /// </summary>
        public string Source { get; }
        /// <summary>
        /// Folder where repositories are stored by GoM.
        /// </summary>
        public string ReposPath { get; }
        /// <summary>
        /// Repository instance of source.
        /// </summary>
        public Repository Repository { get; set; }
        /// <summary>
        /// Path to the repository downloaded.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Uri instance of source if repository is founded on internet.
        /// </summary>
        public Uri Url { get; }

        public Communicator(string source)
        {
            ReposPath = REPOS_DIRECTORY;
            Source = source;
            Url = new Uri(source);
            loadRepository();
        }

        /// <summary>
        /// Load Repository instance of source.
        /// </summary>
        /// <returns>Repository</returns>
        public Repository loadRepository()
        {
            Repository repo = null;
            if (Source.Substring(0, 5) == "https")
            {
                //Parse repository name
                string repoFullName = Helpers.ParseUrl(Source, Helpers.UrlShape.Fullname);
                string repoName = Helpers.ParseUrl(Source, Helpers.UrlShape.Name);

                string tmp_downloading_file_indicator = this.ReposPath + "/downloading_" + repoFullName + "_repository";

                string path = ReposPath + "/" + repoName;

                bool RepoExist = Directory.Exists(path);

                this.Path = path;
                
                //Return repository if already stored
                if (RepoExist && !File.Exists(tmp_downloading_file_indicator))
                {
                    repo = new Repository(path);
                    this.Repository = repo;
                    return repo;
                }


                File.CreateText(tmp_downloading_file_indicator).Close();
                //Clone and return repository if not stored
                Repository.Clone(Source, path);
                repo = new Repository(path);
                this.Repository = repo;
                File.Delete(tmp_downloading_file_indicator);
                return repo;
            }
            else
            {
                //Check repository exist
                bool directoryExist = Directory.Exists(Source);
                if (!directoryExist) return null;

                this.Path = Source;

                //Return if exist
                repo = new Repository(Source);
                this.Repository = repo;
                return repo;
            }
        }

        /// <summary>
        /// Get all files in repository.
        /// </summary>
        /// <param name="searchPattern">Model of search</param>
        /// <returns>All files</returns>
        public List<string> getFiles(string searchPattern ="*") { return Helpers.getAllFilesInDirectory(this.Path, searchPattern);  }
        
        /// <summary>
        /// Get all Folders in repository.
        /// </summary>
        /// <param name="searchPattern">Model of search</param>
        /// <returns>All folders</returns>
        public List<string> getFolders(string searchPattern = "*") { return Helpers.getAllFoldersInDirectory(this.Path, searchPattern); }
        
        /// <summary>
        /// Get BasicGitRepository instance of repository
        /// </summary>
        /// <returns>BasicGitRepository</returns>
        public BasicGitRepository getBasicGitRepository()
        {
            BasicGitRepository BasicGitRepo = new BasicGitRepository() { Url = Url, Path = Path };

            GitRepository gitRepo = new GitRepository() { Url = BasicGitRepo.Url, Path = BasicGitRepo.Path };

            return BasicGitRepo;
        }

        /// <summary>
        /// Get All Branches of repository
        /// </summary>
        /// <returns>List<BasicGitBranch></returns>
        public List<BasicGitBranch> getAllBranches()
        {
            List<BasicGitBranch> branches = new List<BasicGitBranch>();
            using (Repository)
            {
                foreach (var branch in Repository.Branches)
                {
                    branches.Add(convertBranchToGitBranch(branch));
                }
            }

            return branches;
        }

        private BasicGitBranch convertBranchToGitBranch(Branch branch)
        {
            string branchName = branch.CanonicalName;

            Mutable.GitBranch gitBranch = new GitBranch() { Name = branchName, Version = getBranchVersionInfo(branch) };

            Mutable.BasicGitBranch basicGitBranch = new BasicGitBranch() { Name = branchName, Details = gitBranch };

            return basicGitBranch;
        }

        
        private BranchVersionInfo getBranchVersionInfo(Branch branch)
        {
            Mutable.VersionTag versionTag = new VersionTag();
            BranchVersionInfo branchVersionInfo = new BranchVersionInfo();
            int depth = 0;

            // good ?
            foreach (var commit in branch.Commits)
            {
                foreach (var tag in Repository.Tags)
                {
                    if (commit.Sha.ToString().Equals(tag.Target.Sha.ToString()))
                    {
                        versionTag.FullName = tag.FriendlyName;
                        branchVersionInfo.LastTagDepth = depth;
                        branchVersionInfo.LastTag = versionTag;
                    }
                }
                depth++;
            }

            return branchVersionInfo;
        }

        private Project getProject()
        {
            throw new NotImplementedException();
        }

        private List<Project> getProjects()
        {
            throw new NotImplementedException();
        }

        private Target getTarget()
        {
            throw new NotImplementedException();
        }

        private TargetDependency getTargetDependency()
        {
            throw new NotImplementedException();
        }

        private List<Target> getTargets()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get a ExtensionStatictic Dictionary.
        /// </summary>
        /// <returns>Dictionary<String, ExtensionStatistic></returns>
        public Dictionary<String, ExtensionFileStatistic> getExtensionDictionary()
        {

            List<String> allFiles = getFiles();
            Dictionary<String, ExtensionFileStatistic> extensionDictionary = new Dictionary<string, ExtensionFileStatistic>();
            foreach (var file in allFiles)
            {
                String[] splitFolder = file.Split('\\');
                String[] splitExtension = splitFolder[splitFolder.Length - 1].Split('.');
                String ext;
                if (splitExtension.Length > 1)
                {
                    ext = splitExtension[splitExtension.Length - 1];
                    if (!extensionDictionary.ContainsKey(ext))
                    {
                        List<String> allExtensionsFile = getFiles("*." + ext);
                        ExtensionFileStatistic extStat = new ExtensionFileStatistic();
                        extStat.extension = ext;
                        extStat.count = allExtensionsFile.Count;
                        extStat.listPath = allExtensionsFile;
                        extensionDictionary.Add(ext, extStat);
                    }
                }
                else
                {
                    ext = "";
                    if (!extensionDictionary.ContainsKey(ext))
                    {
                        ExtensionFileStatistic extStat = new ExtensionFileStatistic();
                        extStat.extension = ext;
                        extStat.count = 0;
                        extStat.listPath = new List<string>();
                        extensionDictionary.Add(ext, extStat);
                    }
                    ExtensionFileStatistic stat = extensionDictionary[ext];
                    stat.count++;
                    stat.listPath.Add(file);
                }
            }
            return extensionDictionary;
        }


        //Implement others methods..

    }
}
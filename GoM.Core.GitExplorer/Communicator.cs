using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using GoM.Core.Mutable;
using GoM.GitFileProvider;
using Microsoft.Extensions.FileProviders;
/*
 *  A faire : 
 *  + Statistiques sur les extensions de fichiers ( nombre de fichiers par extension)
 *  + Déterminer le type de projet (application c# windows, android, ios, web, etc...)
 *  + Déterminer les dépendances utilisées
 *  ...
 * 
 */

namespace GoM.Core.GitExplorer
{
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
        public Repository Repository { get; }
        /// <summary>
        /// Path to the repository downloaded.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Uri instance of source if repository is founded on internet.
        /// </summary>
        public Uri Url { get; }

        public GitFileProvider.GitFileProvider FileProvider { get; }

        public Communicator(string source)
        {
            ReposPath = REPOS_DIRECTORY;
            Source = source;
            Url = new Uri(source);
            Repository = loadRepository();
            FileProvider = new GitFileProvider.GitFileProvider(Directory.GetCurrentDirectory()+"\\"+Path);

        }

        /// <summary>
        /// Check if source was a git repository
        /// </summary>
        /// <returns></returns>
        public bool isRepository()
        {
            return Repository != null;
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

                string path = ReposPath + "/" + repoName;

                bool RepoExist = Directory.Exists(path);

                this.Path = path;
                
                //Return repository if already stored
                if (RepoExist)
                {
                    repo = new Repository(path);
                    return repo;
                }

                //Clone and return repository if not stored
                Repository.Clone(Source, path);
                repo = new Repository(path);
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
        /// Get All Branches from DirectoryContents
        /// </summary>
        /// <returns>List<BasicGitBranch></returns>
        public IDirectoryContents directoryContents()
        {
            return FileProvider.GetDirectoryContents("branches");
        }

        public IEnumerable<string> getAllBranchesName()
        {
           IDirectoryContents branches = directoryContents();
           return branches.Select(c => c.Name);
        }

        public List<BasicGitBranch> getAllBranches()
        {
            List<BasicGitBranch> branches = new List<BasicGitBranch>();

            using (Repository)
            {
                
                foreach (var branch in directoryContents().ToList())
                {

                    Branch br = Repository.Branches.Where(b => b.FriendlyName.Equals(branch.Name))
                                .First();
                    branches.Add(convertBranchToGitBranch(br));
                }
               
            }
           
           
            return branches;
        }

        private BasicGitBranch convertBranchToGitBranch(Branch branch)
        {
            string branchName = branch.FriendlyName;

            Mutable.GitBranch gitBranch = new GitBranch() { Name = branchName, Version = getBranchVersionInfo(branch) };

            Mutable.BasicGitBranch basicGitBranch = new BasicGitBranch() { Name = branchName, Details = gitBranch };

            return basicGitBranch;
        }

        
        private BranchVersionInfo getBranchVersionInfo(Branch branch)
        {
            Mutable.VersionTag versionTag = new VersionTag();
            BranchVersionInfo branchVersionInfo = new BranchVersionInfo();
            int depth = branch.Commits.Count();

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
                        return branchVersionInfo;
                    }
                }
                depth--;
            }

            return branchVersionInfo;
        }

        public List<Commit> getCommitAncestor(Commit commit)
        {
            List<Commit> result = new List<Commit>();
            foreach (var p in commit.Parents)
            {
                result.AddRange(getCommitAncestor(p));
            }
            return result;
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

        //Implement others methods..

    }
}
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

        private Uri url;
        private GitFileProvider.GitFileProvider fileProvider;

        /// <summary>
        /// origin Url or Local path of repository.
        /// </summary>
        public string Source { get; }
        /// <summary>
        /// Folder where repositories are stored by GoM.
        /// </summary>
        public string ReposPath { get; }

        /// <summary>
        /// Path to the repository downloaded.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Uri instance of source if repository is founded on internet.
        /// </summary>
        public Uri Url { get => url; }

        public RepositoryWrapper RepoWrap { get; }

        public GitFileProvider.GitFileProvider FileProvider { get => fileProvider; }

        public Communicator(string source)
        {
            RepoWrap = new RepositoryWrapper();
            ReposPath = REPOS_DIRECTORY;
            Source = source;
            loadRepository(source);
        }

        /// <summary>
        /// Load Repository instance of source.
        /// </summary>
        /// <returns>Repository</returns>
        public Repository loadRepository(string source)
        {
            Repository repo = null;
            if (source.Substring(0, 5) == "https")
            {
                //Parse repository name
                string repoFullName = Helpers.ParseUrl(source, Helpers.UrlShape.Fullname);
                string repoName = Helpers.ParseUrl(source, Helpers.UrlShape.Name);

                string path = ReposPath + "/" + repoName;

                // return null if Source is invalid
                if (!Helpers.SourceIsValid(source)) return null;

                bool RepoExist = Directory.Exists(path);

                Path = path;

                fileProvider = new GitFileProvider.GitFileProvider(Directory.GetCurrentDirectory() + "\\" + Path);

                //Return repository if already stored
                if (RepoExist)
                {
                    RepoWrap.Create(path);
                    return repo;
                }

                url = new Uri(source);

                //Clone and return repository if not stored
                Repository.Clone(source, path);
                RepoWrap.Create(path);
                return repo;
            }
            else
            {
                Path = source;

                fileProvider = new GitFileProvider.GitFileProvider(Path);

                //Check repository exist
                bool directoryExist = Directory.Exists(source);
                if (!directoryExist) return null;


                //Return if exist
                RepoWrap.Create(source);

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

        public List<BasicGitBranchDecorator> getAllBranches()
        {
            List<BasicGitBranchDecorator> branches = new List<BasicGitBranchDecorator>();
            
            using (loadRepository(Path))
            {
                foreach (var branch in RepoWrap.Repo.Branches)
                    branches.Add(convertBranchToGitBranch(branch));
            }
            return branches;
        }

        private BasicGitBranchDecorator convertBranchToGitBranch(Branch branch)
        {
            string branchName = branch.FriendlyName;

            Mutable.GitBranch gitBranch = new GitBranch() { Name = branchName, Version = getBranchVersionInfo(branch) };

            Mutable.BasicGitBranch basicGitBranch = new BasicGitBranch() { Name = branchName, Details = gitBranch };

            BasicGitBranchDecorator basicGitBranchDeco = new BasicGitBranchDecorator(basicGitBranch, RepoWrap);

            return basicGitBranchDeco;
        }

        
        private BranchVersionInfo getBranchVersionInfo(Branch branch)
        {
            Mutable.VersionTag versionTag = new VersionTag();
            BranchVersionInfo branchVersionInfo = new BranchVersionInfo();
            int depth = branch.Commits.Count();

         //   if (RepoWrap.Repo.Tags.Count() == 0) return branchVersionInfo;
            
                foreach (var commit in branch.Commits)
                {
                    foreach (var tag in RepoWrap.Repo.Tags)
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

        // Not used yet, in which case can we use it ?
        private List<Commit> getCommitAncestor(Commit commit)
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

        public void Fecth()
        {
            string logMessage = "";
            using (RepositoryWrapper rw = new RepositoryWrapper())
            {
                rw.Create(Path);
                FetchOptions option = new FetchOptions();
                foreach (Remote remote in rw.Repo.Network.Remotes)
                {
                    IEnumerable<string> refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                    Commands.Fetch(rw.Repo, remote.Name, refSpecs, null, logMessage);
                }
            }
        }

        //Implement others methods..

    }
}
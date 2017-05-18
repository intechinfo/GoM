using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using GoM.Core.Mutable;
using GoM.GitFileProvider;
using Microsoft.Extensions.FileProviders;
using GoM.Core.FSAnalyzer;
namespace GoM.Core.GitExplorer
{
    public class Communicator : ICommunicator
    {
        const string REPOS_DIRECTORY = "../../repos";

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
        
        public GitFileProvider.GitFileProvider FileProvider { get => fileProvider; }

        public Communicator(string source)
        {
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
                    repo = new Repository(path);
                    return repo;
                }

                url = new Uri(source);

                //Clone and return repository if not stored
                Repository.Clone(source, path);
                repo = new Repository(path);
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
                repo = new Repository(source);

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
        /// Get All Branches from DirectoryContents
        /// </summary>
        /// <returns>List<BasicGitBranch></returns>
        public IDirectoryContents directoryContents()
        {
            return FileProvider.GetDirectoryContents("branches");
        }

        /// <summary>
        /// Get BasicGitRepository instance of repository
        /// </summary>
        /// <returns>BasicGitRepository</returns>
        public BasicGitRepository getBasicGitRepository()
        {
            BasicGitRepository basicGetRepo = new BasicGitRepository() { Url = url, Path = Path };

            using (Repository repo = loadRepository(Path))
            {
                GitRepository gitRepo = new GitRepository() { Url = url, Path = Path };
                gitRepo.Details.Branches.AddRange(getAllBranches());

                basicGetRepo.Details = gitRepo;
            }

            return basicGetRepo;
        }

        public IEnumerable<string> getAllBranchesName()
        {
           IDirectoryContents branches = directoryContents();
           return branches.Select(c => c.Name);
        }

        public List<BasicGitBranch> getAllBranches()
        {
            List<BasicGitBranch> branches = new List<BasicGitBranch>();
            
            using (Repository repo = loadRepository(Path))
            {
                foreach (var branch in repo.Branches)
                    branches.Add(convertBranchToGitBranch(branch, repo));
            }
            return branches;
        }

        private BasicGitBranch convertBranchToGitBranch(Branch branch, Repository repo)
        {
            string branchName = branch.FriendlyName;

            Mutable.GitBranch gitBranch = new GitBranch() { Name = branchName, Version = getBranchVersionInfo(branch, repo) };

            gitBranch.Projects.AddRange(getProjects());

            Mutable.BasicGitBranch basicGitBranch = new BasicGitBranch() { Name = branchName, Details = gitBranch };

            return basicGitBranch;
        }

        private BranchVersionInfo getBranchVersionInfo(Branch branch, Repository repo)
        {
            Mutable.VersionTag versionTag = new VersionTag();
            BranchVersionInfo branchVersionInfo = new BranchVersionInfo();
            int depth = branch.Commits.Count();

         //   if (RepoWrap.Repo.Tags.Count() == 0) return branchVersionInfo;
            
            foreach (var commit in branch.Commits)
            {
                foreach (var tag in repo.Tags)
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

        private List<BasicProject> getProjects()
        {
            ProjectFolderController projectFolderController = new ProjectFolderController();
            List<BasicProject> basicProjects = new List<BasicProject>();
            foreach (var project in projectFolderController.Analyze(fileProvider).ToList())
            {
                Project p = new Project(project);
                BasicProject basicProject = new BasicProject() { Path = project.Path, Details = p };
                basicProjects.Add(basicProject);
            }

            return basicProjects;
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

        public void Fetch()
        {
            string logMessage = "";
            using (var repo = new Repository(Path))
            {
                foreach (Remote remote in repo.Network.Remotes)
                {
                    IEnumerable<string> refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                    Commands.Fetch(repo, remote.Name, refSpecs, null, logMessage);
                }
            }
            Console.WriteLine(logMessage);
        }

        //Implement others methods..

    }
}
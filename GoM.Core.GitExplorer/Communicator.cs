using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LibGit2Sharp;
using GoM.Core.Mutable;

namespace GoM.Core.GitExplorer
{
    public class Communicator
    {
        const string REPOS_DIRECTORY = "repos";

        public string Source { get; set; }
        public string ReposPath { get; set; }
        public Repository Repository { get; set; }
        public string Path { get; set; }
        public Uri Url { get; set; }

        public Communicator(string source)
        {
            ReposPath = REPOS_DIRECTORY;
            Source = source;
            loadRepository();
        }

        public Repository loadRepository()
        {
            Repository repo = null;
            if (Source.Substring(0, 5) == "https")
            {
                //Parse repository name
                string repoFullName = Helpers.ParseUrl(Source, Helpers.UrlShape.Fullname);
                string repoName = Helpers.ParseUrl(Source, Helpers.UrlShape.Name);

                string path = ReposPath + "/" + repoName;

                bool fileExist = File.Exists(path);

                this.Path = path;

                this.Url = new Uri(this.Source);

                //Return repository if already stored
                if (fileExist)
                {
                    repo = new Repository(path);
                    this.Repository = repo;
                    return repo;
                }

                //Clone and return repository if not stored
                Repository.Clone(Path, path);
                repo = new Repository(path);
                this.Repository = repo;
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

        // Good ?
        public BasicGitRepository getBasicGitRepository()
        {
            BasicGitRepository BasicGitRepo = new BasicGitRepository();
            BasicGitRepo.Path = this.Path;
            BasicGitRepo.Url = this.Url;

            GitRepository gitRepo = new GitRepository();
            gitRepo.Url = BasicGitRepo.Url;
            gitRepo.Path = BasicGitRepo.Path;

            return BasicGitRepo;
        }

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

            Mutable.GitBranch gitBranch = new GitBranch();
            gitBranch.Name = branchName;
            gitBranch.Version = getBranchVersionInfo(branch);

            Mutable.BasicGitBranch basicGitBranch = new BasicGitBranch();
            basicGitBranch.Name = branchName;
            basicGitBranch.Details = gitBranch;

            return basicGitBranch;
        }

        // good ?
        private BranchVersionInfo getBranchVersionInfo(Branch branch)
        {
            Mutable.VersionTag versionTag = new VersionTag();
            BranchVersionInfo branchVersionInfo = new BranchVersionInfo();
            int depth = 0;
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

        //Implement others methods..

    }
}
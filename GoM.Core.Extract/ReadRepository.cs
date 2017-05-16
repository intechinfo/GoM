using System;
using LibGit2Sharp;
using System.Linq;
using System.Collections;
using GoM.Core.Mutable;
using System.Collections.Generic;

namespace GoM.Core.Extract
{
    public class ReadRepository
    {
        String _uri;
        Repository repo;

        public ReadRepository(String uri)
        {
            this._uri = uri;
            if (gitRepositoryExist())
            {
                repo = new Repository(this.Uri);
            }
        }

        public String Uri { get => _uri; }

        private bool gitRepositoryExist()
        {
            return Repository.IsValid(this.Uri);
        }

        private Branch getBranch(String name)
        {
            return this.repo.Branches[name];
        }

        private List<Branch> getAllBranch()
        {
            return this.repo.Branches.ToList();
        }
        
        public BranchVersionInfo getBranchVersionInfo(String branchName)
        {
            BranchVersionInfo branchVersionInfo = null;

            var branch = getBranch(branchName);
            var commits = branch.Commits.ToList();

            var allTags = this.repo.Tags.ToList();

            allTags.Reverse();
            foreach (var t in allTags)
            {
                var target = t.Target;
                String targetCommit = target.ToString();
                Commit commit = repo.Lookup<Commit>(targetCommit);
                List<Commit> recursiveAncestors = getCommitAncestor(commit);
                if (commits.Contains(commit)){
                    branchVersionInfo = new BranchVersionInfo();
                    List<Commit> listCommitWithoutTag = commits.Where<Commit>(x => !recursiveAncestors.Contains(x)).ToList();
                    VersionTag versionTag = new VersionTag();
                    versionTag.FullName = targetCommit;
                    branchVersionInfo.LastTag = versionTag;
                    branchVersionInfo.LastTagDepth = listCommitWithoutTag.Count;

                    return branchVersionInfo;
                }
            }
            return branchVersionInfo;
        }
        
        private List<Commit> getCommitAncestor(Commit commit)
        {
            List<Commit> result = new List<Commit>();
            foreach (var p in commit.Parents)
            {
                result.AddRange(getCommitAncestor(p));
            }
            return result;
        }

        public List<GitBranch> getListGitBranch()
        {
            List<GitBranch> listGitBranch = new List<GitBranch>();
            List<Branch> branchs = getAllBranch();
            foreach (var b in branchs)
            {
                GitBranch gitBranch = new GitBranch();
                gitBranch.Name = b.CanonicalName;
                gitBranch.Version = getBranchVersionInfo(b.CanonicalName);
                listGitBranch.Add(gitBranch);
            }
            return listGitBranch;
        }
    }
}

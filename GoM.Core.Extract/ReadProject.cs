using System;
using LibGit2Sharp;
using System.Linq;
using System.Collections;
using GoM.Core.Mutable;

namespace GoM.Core.Extract
{
    public class ReadProject
    {
        String _uri;

        public ReadProject(String uri)
        {
            this._uri = uri;
        }

        public String Uri { get => _uri; }

        public bool gitRepositoryExist()
        {
            return Repository.IsValid(this.Uri);
        }

        public Branch getBranch(String name)
        {
            Repository repo = new Repository(this.Uri);
            var branch = repo.Branches[name];
            var allTags = repo.Tags.ToList();
            var aTag = repo.Tags["refs/tags/v1.1"];
            var bTag = aTag.IsAnnotated;

            return branch;
        }

        public ArrayList getAllBranch()
        {
            ArrayList branchs = new ArrayList();
            Repository repo = new Repository(this.Uri);
            var allBranchs = repo.Branches.ToList();
            return branchs;
        }
        
        public String getBranchVersionInfo(String branchName)
        {
            BranchVersionInfo branchVersionInfo = null;

            Repository repo = new Repository(this.Uri);
            var branch = repo.Branches[branchName];
            var commits = branch.Commits.ToList();

            var allTags = repo.Tags.ToList();

            allTags.Reverse();
            foreach (var t in allTags)
            {
                var target = t.Target;
                String targetCommit = target.ToString();
                Commit commit = repo.Lookup<Commit>(targetCommit);
                //var recursiveAncestors = getRecursiveParents(commit);
                if (commits.Contains(commit)){
                    
                    branchVersionInfo = new BranchVersionInfo();
                    VersionTag versionTag = new VersionTag();
                    versionTag.FullName = targetCommit;
                    branchVersionInfo.LastTag = versionTag;
                    

                }
            }

            return null;
        }
        
    }
}

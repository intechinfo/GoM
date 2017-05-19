using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;
using LibGit2Sharp;
using System.Text.RegularExpressions;

enum TYPE { Unhandled = -1, Root = 0, Branches, Tags, Commits, Head };
namespace GoM.GitFileProvider
{
    public class GitFileProvider : IFileProvider
    {
        readonly string _rootPath;
        readonly bool _exist;
        const string INVALID_PATH = "Invalid path";
        const string INVALID_BRANCH = "The branch don't exist in the repository";
        const string INVALID_TAG = "The tag don't exist in the repository";
        const string INVALID_COMMIT = "The commit don't exist in the repository";
        const string INVALID_REPOSITORY = "The Repository doesn't exist";
        const string INVALID_HEAD = "The repository doesn't contain head";
        const string INVALID_COMMAND = "The command doesn't exist";

        /// <summary>
        /// Initialize GitFileProvider for a specific repository
        /// </summary>
        /// <param name="rootPath">The physical path of the repository</param>
        public GitFileProvider(string rootPath)
        {
            _rootPath = rootPath;
            _exist = IsCorrectGitDirectory();
        }
        /// <summary>
        /// Return <see cref="IDirectoryContents"/>
        /// </summary>
        /// <param name="subpath"></param>
        /// <returns></returns>
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            if (subpath == null) return NotFoundDirectoryContents.Singleton;
            if (!_exist) return NotFoundDirectoryContents.Singleton;
            string[] splitPath = PathDecomposition(subpath, out TYPE type, out char flag);
            switch (type)
            {
                case TYPE.Unhandled:
                    return GetDirectoryHead(splitPath, subpath);
                case TYPE.Root:
                    return GetDirectoryRoot();
                case TYPE.Branches:
                    return GetDirectoryBranch(splitPath, subpath, flag);
                case TYPE.Tags:
                    return GetDirectoryTags(splitPath, subpath, flag);
                case TYPE.Commits:
                    return GetDirectoryCommit(splitPath, subpath, flag);
                case TYPE.Head:
                    return GetDirectoryHead(splitPath, subpath);
                default:
                    return NotFoundDirectoryContents.Singleton;
            }
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (subpath == null)
                return new NotFoundFileInfo(INVALID_COMMAND);
            if (!_exist)
                return new NotFoundFileInfo(INVALID_REPOSITORY);
            string[] splitPath = PathDecomposition(subpath, out TYPE type, out char flag);
            switch (type)
            {
                case TYPE.Unhandled:
                    return new NotFoundFileInfo(INVALID_COMMAND);
                case TYPE.Root:
                    return new FileInfoRefType(_rootPath, INVALID_COMMAND);
                case TYPE.Branches:
                    return GetFileBranch(splitPath, subpath, flag);
                case TYPE.Tags:
                    return GetFileTag(splitPath, subpath, flag);
                case TYPE.Commits:
                    return GetFileCommit(splitPath, subpath, flag);
                case TYPE.Head:
                    return GetFileHead(splitPath, subpath, flag);
                default:
                    return new NotFoundFileInfo(INVALID_COMMAND);
            }
        }

        private bool IsCorrectGitDirectory()
        {
            string fullpath = _rootPath;
            if (!Regex.IsMatch(_rootPath, @".git\\?$"))
            {
                fullpath = fullpath + @"\.git";
            }
            return Directory.Exists(fullpath);
        }

        private string[] PathDecomposition(string subpath, out TYPE type, out char flag)
        {
            string[] decomposition = subpath.Split('\\');
            if (decomposition == null)
            {
                decomposition = new string[1];
                decomposition[0] = subpath.Trim();
            }
            flag = (char)0;

            if (decomposition[0].Equals(""))
            {
                type = TYPE.Root;
            }
            else if (decomposition[0].Equals("branches"))
            {
                type = TYPE.Branches;
            }
            else if (decomposition[0].Equals("tags"))
            {
                type = TYPE.Tags;
            }
            else if (decomposition[0].Equals("commits"))
            {
                type = TYPE.Commits;
            }
            else if (decomposition[0].Equals("head"))
            {
                type = TYPE.Head;
            }
            else
                type = TYPE.Unhandled;

            if (decomposition.Length <= 1 || decomposition[1].Equals("*"))
                flag = '*';
            else
                flag = 'n';
            return decomposition;
        }

        private IDirectoryContents CreateDirectoryInfo(Tree tree, RepositoryWrapper rw, string relativePath)
        {
            if (tree == null) return NotFoundDirectoryContents.Singleton;
            List<IFileInfo> files = new List<IFileInfo>();
            if (relativePath != "" && !relativePath.LastOrDefault().Equals(Path.DirectorySeparatorChar)) relativePath = relativePath + Path.DirectorySeparatorChar;
            foreach (var file in tree)
            {
                IFileInfo f = file.TargetType != TreeEntryTargetType.Blob ? new FileInfoDirectory(true, relativePath + file.Name, file.Name) as IFileInfo
                                                          : new FileInfoFile(true, relativePath, file.Name, DateTimeOffset.MinValue, file.Target as Blob, rw, true);
                files.Add(f);
            }
            DirectoryInfo fDir = new DirectoryInfo(files);
            return fDir;
        }

        private IDirectoryContents GetDirectoryBranch(string[] splitPath, string subPath, char flag)
        {
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                if (flag == '*')
                {
                    List<IFileInfo> files = new List<IFileInfo>();
                    foreach (var b in rw.Repo.Branches)
                    {
                        IFileInfo file = new FileInfoDirectory(true, "branches" + Path.DirectorySeparatorChar + b.FriendlyName, b.FriendlyName);
                        files.Add(file);
                    }
                    return new DirectoryInfo(files);
                }
                Branch branch = rw.Repo.Branches.FirstOrDefault(c => c.FriendlyName == splitPath[1]);
                if (branch == null || branch.Tip == null)
                    return NotFoundDirectoryContents.Singleton;
                string relativePath = GetRelativePath(splitPath);
                if (String.IsNullOrEmpty(relativePath))
                    return CreateDirectoryInfo(branch.Tip.Tree, rw, relativePath);
                var dir = branch.Tip?.Tree[relativePath];
                if (dir?.TargetType != TreeEntryTargetType.Tree) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(dir.Target as Tree, rw, relativePath);
            }
        }

        private IDirectoryContents GetDirectoryRoot()
        {
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                Branch head = rw.Repo.Head;
                if (head == null || head.Tip == null) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(head.Tip.Tree, rw, "");
            }
        }

        private IDirectoryContents GetDirectoryCommit(string[] splitPath, string subPath, char flag)
        {
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                if (flag == '*')
                {
                    List<IFileInfo> files = new List<IFileInfo>();
                    foreach (var b in rw.Repo.Commits)
                    {
                        IFileInfo file = new FileInfoDirectory(true, "commits" + Path.DirectorySeparatorChar + b.Message, b.Message);
                        files.Add(file);
                    }
                    return new DirectoryInfo(files);
                }
                Commit commit = rw.Repo.Lookup<Commit>(splitPath[1]);
                if (commit == null) return NotFoundDirectoryContents.Singleton;
                string relativePath = GetRelativePath(splitPath);
                TreeEntry node = commit.Tree[relativePath];
                if (node?.TargetType != TreeEntryTargetType.Tree) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(node.Target as Tree, rw, relativePath);
            }
        }

        private IDirectoryContents GetDirectoryTags(string[] splitPath, string subPath, char flag)
        {
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                if (flag == '*')
                {
                    List<IFileInfo> files = new List<IFileInfo>();
                    foreach (var b in rw.Repo.Tags)
                    {
                        IFileInfo file = new FileInfoDirectory(true, "tags" + Path.DirectorySeparatorChar + b.FriendlyName, b.FriendlyName);
                        files.Add(file);
                    }
                    return new DirectoryInfo(files);
                }
                Tag tag = rw.Repo.Tags.FirstOrDefault(c => c.FriendlyName == splitPath[1]);
                if (tag == null) return NotFoundDirectoryContents.Singleton;
                string relativePath = GetRelativePath(splitPath);
                TreeEntry node = rw.Repo.Lookup<Commit>(tag.Target.Id)?.Tree[relativePath];
                if (node?.TargetType != TreeEntryTargetType.Tree) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(node.Target as Tree, rw, relativePath);
            }
        }

        private IDirectoryContents GetDirectoryHead(string[] splitPath, string subPath)
        {
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                Branch head = rw.Repo.Head;
                if (head == null || head.Tip == null) return NotFoundDirectoryContents.Singleton;
                string relativePath = GetRelativePath(splitPath,1);
                if (String.IsNullOrEmpty(relativePath))
                    return GetDirectoryRoot();
                var dir = head.Tip?.Tree[relativePath];
                if (dir?.TargetType != TreeEntryTargetType.Tree) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(dir.Target as Tree, rw, relativePath);
            }
        }

        private IFileInfo GetFileBranch(string[] splitPath, string subpath, char flag)
        {
            if (flag == '*')
                return new FileInfoRefType(_rootPath + @"\branches", "branches");
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                Branch b = rw.Repo.Branches.ToList().Where(c => c.FriendlyName == splitPath[1]).FirstOrDefault();
                return BranchFileManager(rw, b, splitPath);
            }
        }

        private IFileInfo GetFileCommit(string[] splitPath, string subpath, char flag)
        {
            if (flag == '*')
                return new FileInfoRefType(_rootPath + @"\commits", "commits");
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                string commitHash = splitPath[1];
                Commit commit = rw.Repo.Lookup<Commit>(commitHash);
                if (commit == null)
                    return new NotFoundFileInfo(INVALID_COMMIT);
                string relativePath = GetRelativePath(splitPath);
                if (String.IsNullOrEmpty(relativePath))
                    return new NotFoundFileInfo(INVALID_PATH);
                TreeEntry node = commit.Tree[relativePath];
                if (node == null)
                    return new NotFoundFileInfo(INVALID_PATH);
                if (node.TargetType == TreeEntryTargetType.Tree)
                    return new FileInfoRef(true, -1, relativePath, node.Name, DateTimeOffset.MaxValue, true);
                if (node.TargetType == TreeEntryTargetType.Blob)
                    return new FileInfoFile(true, relativePath, node.Name, commit.Committer.When, node.Target as Blob, rw);
            }
            return new NotFoundFileInfo(INVALID_PATH);
        }

        private IFileInfo GetFileTag(string[] splitPath, string subPath, char flag)
        {
            if (flag == '*')
                return new FileInfoRefType(_rootPath + @"\tags", "tags");
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                Tag tag = rw.Repo.Tags.FirstOrDefault(t => t.FriendlyName == splitPath[1]);
                if (tag == null || tag.Target == null) return new NotFoundFileInfo(INVALID_TAG);
                string relativePath = GetRelativePath(splitPath);
                if (relativePath == null) return new NotFoundFileInfo(INVALID_PATH);
                var commit = rw.Repo.Lookup<Commit>(tag.Target.Sha);
                if (commit == null) return new NotFoundFileInfo(INVALID_TAG);
                var tree = commit.Tree[relativePath];
                if (tree == null) return new NotFoundFileInfo(INVALID_PATH);
                if (tree.TargetType == TreeEntryTargetType.Tree)
                    return new FileInfoRef(true, -1, relativePath, tree.Name, DateTimeOffset.MaxValue, true);
                if (tree.TargetType == TreeEntryTargetType.Blob)
                    return new FileInfoFile(true, relativePath, tree.Name, commit.Committer.When, tree.Target as Blob, rw);
            }
            return new NotFoundFileInfo(INVALID_PATH);
        }

        private IFileInfo GetFileHead(string[] splitPath, string subpath, char flag)
        {
            using (RepositoryWrapper rw = new RepositoryWrapper(_rootPath))
            {
                return BranchFileManager(rw, rw.Repo.Head, splitPath, 1);
            }
        }

        private IFileInfo BranchFileManager(RepositoryWrapper rw, Branch branch, string[] splitPath, int index = 2)
        {
            if (branch == null || branch.Tip == null)
                return new NotFoundFileInfo(INVALID_BRANCH);
            string relativePath = GetRelativePath(splitPath, index);
            if (String.IsNullOrEmpty(relativePath))
                return new NotFoundFileInfo(INVALID_PATH);
            TreeEntry node = branch[relativePath];
            if (node == null)
                return new NotFoundFileInfo(INVALID_PATH);
            if (node.TargetType == TreeEntryTargetType.Tree)
                return new FileInfoRef(true, -1, relativePath, node.Name, DateTimeOffset.MaxValue, true);
            if (node.TargetType == TreeEntryTargetType.Blob)
                return new FileInfoFile(true, relativePath, node.Name, branch.Tip.Committer.When, node.Target as Blob, rw);
            return new NotFoundFileInfo(INVALID_PATH);
        }

        private string GetRelativePath(string[] splitPath, int index = 2)
        {
            string RelativePath = "";
            for (int i = index; i < splitPath.Length; i++)
                RelativePath += splitPath[i] + ((i < splitPath.Length - 1) ? Path.DirectorySeparatorChar.ToString() : "");
            return RelativePath.Trim();
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }

    }
}

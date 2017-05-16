﻿using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using LibGit2Sharp;
using System.Text.RegularExpressions;
using Microsoft.Extensions.FileProviders.Physical;

enum TYPE {Unhandled = -1, Root = 0, Branches, Tags, Commits};
namespace GoM.GitFileProvider
{
    public class GitFileProvider : IFileProvider
    {
        readonly string _rootPath;
        readonly bool _exist;
        readonly GitFilesWatcher _rootWatcher;

        public GitFileProvider(string rootPath)
        {
            _rootPath = rootPath;
            _exist = IsCorrectGitDirectory();
            if (_exist)
            {
                _rootWatcher = new GitFilesWatcher(GetPathToGit(), new System.IO.FileSystemWatcher(GetPathToGit()), false);
               
                WatchGitDirectory().GetAwaiter().GetResult();
            }
        }
        

        private string GetPathToGit()
        {
            string fullpath = _rootPath;
            if (!Regex.IsMatch(_rootPath, @".git\\?$"))
            {
                fullpath = fullpath + @"\.git\";
            }
            return fullpath;
        }
        private bool IsCorrectGitDirectory()
        {
            string fullpath = _rootPath;
            if(!Regex.IsMatch(_rootPath, @".git\\?$"))
            {
                fullpath = fullpath + @"\.git";
            }
            var dir = new System.IO.DirectoryInfo(fullpath);
            if (!dir.Exists) return false;
            return true;
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
            else
                type = TYPE.Unhandled;

            if (decomposition.Length <= 1 || decomposition[1].Equals("*"))
                flag = '*';
            else
                flag = 'n';
            return decomposition;
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            if (!_exist)
                return NotFoundDirectoryContents.Singleton;
            string[] splitPath = PathDecomposition(subpath, out TYPE type, out char flag);
            switch (type)
            {
                case TYPE.Unhandled:
                    return NotFoundDirectoryContents.Singleton;
                case TYPE.Root:
                    return GetDirectoryRoot();
                case TYPE.Branches:
                    return GetDirectoryBranch(splitPath, subpath, flag);
                case TYPE.Tags:
                    break;
                case TYPE.Commits:
                    break;
                default:
                    break;
            }
            return null;
        }

        private DirectoryInfo CreateDirectoryInfo(Tree tree, RepositoryWrapper rw)
        {
            List<IFileInfo> files = new List<IFileInfo>();
            foreach (var file in tree)
            {
                IFileInfo f = file.Mode == Mode.Directory ? new FileInfoDirectory(true, _rootPath + @"\" + file.Name, file.Name) as IFileInfo
                                                          : new FileInfoFile(true, _rootPath + @"\" + file.Name, file.Name, DateTimeOffset.MinValue, file.Target as Blob, rw);
                files.Add(f);
            }
            DirectoryInfo fDir = new DirectoryInfo(files);
            return fDir;
        }

        private IFileInfo GetFileBranch(string[] splitPath, string subpath, char flag)
        {
            if (flag == '*')
                return new FileInfoRefType(_rootPath + @"\branches", "branches");
            else
            {
                using (RepositoryWrapper rw = new RepositoryWrapper())
                {
                    rw.Create(_rootPath);
                    Branch b = rw.Repo.Branches.ToList().Where(c => c.FriendlyName == splitPath[1]).FirstOrDefault();
                    if (b == null)
                        return new NotFoundFileInfo("Invalid"); ; // TODO
                    string pathToFileFromBranch = GetRelativePath(splitPath);
                    if (String.IsNullOrEmpty(pathToFileFromBranch))
                        return new FileInfoRef(true, -1, _rootPath + subpath, b.FriendlyName, default(DateTimeOffset), true);
                    TreeEntry node = b[pathToFileFromBranch];
                    if (node == null)
                        return new NotFoundFileInfo("Invalid");
                    FileInfoFile f = new FileInfoFile(true, _rootPath + @"\" + subpath, splitPath[splitPath.Length - 1], default(DateTimeOffset), (Blob)node.Target, rw);
                    return f;
                }
            }
        }

        private IFileInfo GetFileCommit(string[] splitPath, string subpath, char flag)
        {
            if (flag == '*')
                return new FileInfoRefType(_rootPath + @"\commits", "commits");
            else
            {
                using (RepositoryWrapper rw = new RepositoryWrapper())
                {
                    rw.Create(_rootPath);
                    string commitHash = splitPath[1];
                    Commit commit = rw.Repo.Lookup<Commit>(commitHash);
                    if (commit == null)
                        return new NotFoundFileInfo("InvalidSha");
                    string relativePath = GetRelativePath(splitPath, 3);
                    if (String.IsNullOrEmpty(relativePath))
                    {
                        TreeEntry entry = commit.Tree.FirstOrDefault();
                        if (entry == null)
                        {
                            return new NotFoundFileInfo("NoFile");
                        }
                        else
                        {
                            if (entry.TargetType == TreeEntryTargetType.Blob)
                                return new FileInfoFile(true, null, null, default(DateTimeOffset), entry.Target as Blob, rw);
                        }
                    }
                    TreeEntry node = commit.Tree[relativePath];
                    if (node == null)
                        return new NotFoundFileInfo("InvalidPath");
                    if (node.TargetType == TreeEntryTargetType.Blob)
                        return new FileInfoFile(true, relativePath, splitPath[splitPath.Length - 1], default(DateTimeOffset), node.Target as Blob, rw);
                }
            }
            return null;
        }

        private IDirectoryContents GetDirectoryBranch(string[] splitPath, string subpath, char flag)
        {
            using (RepositoryWrapper rw = new RepositoryWrapper())
            {
                rw.Create(_rootPath);
                Branch branch = rw.Repo.Branches.FirstOrDefault(c => c.FriendlyName == splitPath[1]);
                if (branch == null)
                    return NotFoundDirectoryContents.Singleton;
                string relativePath = GetRelativePath(splitPath);
                if (String.IsNullOrEmpty(relativePath))
                    return CreateDirectoryInfo(branch.Tip.Tree, rw);
                var f = branch.Tip.Tree[relativePath];
                if (f?.TargetType != TreeEntryTargetType.Tree) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(f.Target as Tree, rw);
            }
        }

        private IDirectoryContents GetDirectoryRoot()
        {
            using (RepositoryWrapper rw = new RepositoryWrapper())
            {
                rw.Create(_rootPath);
                Branch head = rw.Repo.Head;
                if (head == null) return NotFoundDirectoryContents.Singleton;
                return CreateDirectoryInfo(head.Tip.Tree, rw);
            }
        }

        private string GetRelativePath(string[] splitPath, int offset = 2)
        {
            string RelativePath = "";
            for (int i = 2; i < splitPath.Length; i++)
                RelativePath += splitPath[i] + ((i < splitPath.Length - 1) ? @"\" : "");
            return RelativePath.Trim();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (subpath == null)
                return new NotFoundFileInfo("Invalid");

            TYPE type;
            char flag;
            string[] splitPath = PathDecomposition(subpath, out type, out flag);
            if (!_exist)
                return new NotFoundFileInfo("Invalid");
            switch (type)
            {
                case TYPE.Unhandled:
                    return new NotFoundFileInfo("Invalid");
                case TYPE.Root:
                    return new FileInfoRefType(_rootPath, "root");
                case TYPE.Branches:
                    return GetFileBranch(splitPath, subpath, flag);
                case TYPE.Tags:
                    return null; // TODO
                case TYPE.Commits:
                    return GetFileCommit(splitPath, subpath, flag);
                default:
                    return null; // TODO
            }
        }

        public bool IsValidFilter(string filter)
        {
            // TODO
            return true;
        }

        public IChangeToken Watch(string filter)
        {
            if (!IsValidFilter(filter))
                return NullChangeToken.Singleton;
            return null;
        }

    }
}

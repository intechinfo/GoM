using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using LibGit2Sharp;
using System.Text.RegularExpressions;

enum TYPE {Unhandled = -1, Root = 0, Branches, Tags, Commits};
namespace GoM.GitFileProvider
{
    public class GitFileProvider : IFileProvider
    {
        readonly string _rootPath;
        readonly bool _exist;
        readonly RepositoryWrapper _repoWrapper;


        public GitFileProvider(string rootPath)
        {
            _rootPath = rootPath;
            _exist = IsCorrectGitDirectory();
            _repoWrapper = new RepositoryWrapper();
        }

        private bool IsCorrectGitDirectory()
        {
            string fullpath = _rootPath;
            if(!Regex.IsMatch(_rootPath, @".git\\?$"))
            {
                fullpath = fullpath + @"\.git";
            }
            var dir = new DirectoryInfo(fullpath);
            if (!dir.Exists) return false;
            return true;
        }

        private string[] PathDecomposition(string subpath, out TYPE type, out char flag)
        {
            string[] decomposition = subpath.Split('\\');
            flag = (char)0;
            if (decomposition.Length == 0)
            {
                type = TYPE.Root;
            }
            else if (decomposition[0].Equals("branches"))
            {
                type = TYPE.Branches;
                if (decomposition.Length <= 1 || decomposition[1].Equals("*"))
                    flag = '*'; 
                else
                    flag = 'n'; 
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
                    using (Repository repo = _repoWrapper.Create(_rootPath))
                    {
                        Branch head = repo.Head;
                        if (head == null) return NotFoundDirectoryContents.Singleton;
                        var dircontent = head.Tip.Tree;
                    }
                    break;
                case TYPE.Branches:
                    break;
                case TYPE.Tags:
                    break;
                case TYPE.Commits:
                    break;
                default:
                    break;
            }
            return null;
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            TYPE type;
            char flag;
            string[] splitPath = PathDecomposition(subpath, out type, out flag);
            if (!_exist)
                return new NotFoundFileInfo(splitPath.Length == 0 ? "unknown file" : splitPath[splitPath.Length-1]);
            switch (type)
            {
                case TYPE.Unhandled:
                    return new NotFoundFileInfo(splitPath.Length == 0 ? "unknown file" : splitPath[splitPath.Length - 1]);
                case TYPE.Root:
                    FileInfoRefType root = new FileInfoRefType(subpath, splitPath[splitPath.Length - 1]);
                    return root;
                case TYPE.Branches:
                    if (flag == '*')
                        return new FileInfoRefType(subpath, "branches");
                    else
                    {
                        using(Repository repo = _repoWrapper.Create(_rootPath))
                        {
                            Branch b = repo.Branches.Where(c => c.FriendlyName == splitPath[1]).FirstOrDefault();
                            if (b == null)
                                return null; // TODO
                            string pathToFileFromBranch = "";
                            for (int i = 2; i < splitPath.Length - 2; i++)
                                pathToFileFromBranch += splitPath[i] + @"\";
                            pathToFileFromBranch += splitPath[splitPath.Length - 1];
                            TreeEntry node = b[pathToFileFromBranch];
                            if (node == null)
                                return new FileInfoFile(false, -1, null, null, default(DateTimeOffset), false);
                            return new FileInfoFile(true, 0, _rootPath + @"\" + subpath, splitPath[splitPath.Length - 1], default(DateTimeOffset), (node.Mode == Mode.Directory));
                        }
                    }
                case TYPE.Tags:
                    return null; // TODO
                case TYPE.Commits:
                    return null; // TODO
                default:
                    return null;
            }
        }

        public IChangeToken Watch(string filter)
        {
            throw new NotImplementedException();
        }

    }
}

using System;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using GoM.Core.Persistence;
using GoM.Core.GitExplorer;

namespace GoM
{
    class Program
    {
        public static void Main(params string[] args)
        {
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.Name = "GoM";


            app.HelpOption("-h|--help");

            app.OnExecute(() =>
            {
                app.ShowHelp("gom");
                return 0;
            });

            app.Command("add", (command) =>
            {
                command.Description = "This command allow to the user to add repository and projects to his GoM";
                command.HelpOption("-h|--help");

                CommandOption repositoryOption = command.Option("-r|--repository",
                    "Repository to add to GoM"
                    , CommandOptionType.NoValue);

                CommandOption projectOption = command.Option("-p|--project",
                    "Project to add to the repository",
                    CommandOptionType.NoValue);

                CommandOption allProjectOption = command.Option("-p -all|--project -all",
                   "Add all the projects to the repository",
                   CommandOptionType.NoValue);

                CommandOption branchOption = command.Option("-b|--branch",
                    "Add a branch to GoM",
                    CommandOptionType.NoValue);

                CommandArgument projectLocationArgument = command.Argument("[location]", "Where the projects should be located");
                var path = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : Directory.GetCurrentDirectory();
                var name = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : null;

                command.OnExecute(() =>
                {
                    int repos = repositoryOption.Values.Count;
                    int proj = projectOption.Values.Count;
                    int allProj = allProjectOption.Values.Count;
                    int branch = branchOption.Values.Count;


                    // add repository
                    if (repos > 0)
                    {
                        Communicator com = new Communicator(path);
                    }
                    // add branch
                    else if (branch > 0)
                    {
                        if (name != null)
                        {
                            Communicator com = new Communicator(Directory.GetCurrentDirectory());
                            var branches = com.getAllBranches();
                            bool isBranchExist = false;

                            foreach (var b in branches)
                            {
                                if (b.Name == name)
                                {
                                    isBranchExist = true;
                                }
                            }

                            if (isBranchExist) Console.WriteLine("This branch already exist");
                            else
                            {
                                //Create BasicGitBranch
                            }

                        }
                    }
                    // add project
                    else if (allProj > 0)
                    {

                    }
                    // add all project
                    else
                    {

                    }

                    Console.WriteLine();
                    return 0;
                });
            });

            /// This command allow to exclude from GoM the repo/branch/project
            app.Command("remove", (command) =>
            {
                command.Description = "Exclude repository/branch/project";
                command.HelpOption("-h|--help");

                var excludeRepoOPtion = command.Option("-r|--repository",
                    "Exclude a repository from GoM",
                    CommandOptionType.NoValue);

                var excludeBranchOPtion = command.Option("-b|--branch",
                   "Exclude one branch from GoM",
                   CommandOptionType.NoValue);

                var excludeAllBranchOPtion = command.Option("-b -all|--branch -all",
                  "Exclude all branches from GoM",
                  CommandOptionType.NoValue);

                var excludeProjectOPtion = command.Option("-p|--project",
                  "Exclude one project from GoM",
                  CommandOptionType.NoValue);

                var excludeAllProjectshOPtion = command.Option("-p -all|--projects -all",
                  "Exclude all projects from GoM",
                  CommandOptionType.NoValue);

                CommandArgument projectLocationArgument = command.Argument("[location]", "Where the projects should be located");

                int repo = excludeRepoOPtion.Values.Count;
                int branch = excludeBranchOPtion.Values.Count;
                int allBranches = excludeAllBranchOPtion.Values.Count;
                int project = excludeProjectOPtion.Values.Count;
                int allProjects = excludeAllProjectshOPtion.Values.Count;

                var path = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : Directory.GetCurrentDirectory();

                command.OnExecute(() =>
                {
                    // remove repo
                    if (repo > 0)
                    {
                        Communicator com = new Communicator(path);
                        com.getBasicGitRepository().Details = null;
                        foreach (var file in com.getFiles())
                        {
                            Console.WriteLine(file);
                            Directory.Delete(file);
                        }

                        foreach (var directory in com.getFolders())
                        {
                            Console.WriteLine(directory);
                            Directory.Delete(directory);
                        }
                    }
                    // remove branch
                    else if (branch > 0)
                    {

                    }
                    // remove branch
                    else if (allBranches > 0)
                    {

                    }
                    // remove branch
                    else if (project > 0)
                    {

                    }
                    // remove branch
                    else if (allProjects > 0)
                    {

                    }




                    return 0;
                });
            });

            app.Command("init", (command) =>
            {
                command.Description = "Initialize a new GoM repository in the current directory";
                var myCurrentDirectory = Directory.GetCurrentDirectory();
                command.HelpOption("-h|--help");

                command.OnExecute(() =>
                {
                    string pathFound;
                    var succes = new Persistence().TryInit(myCurrentDirectory, out pathFound);

                    if (succes) Console.WriteLine("GoM repository has been correctly initialized");
                    else Console.WriteLine("GoM repository initialisation failed. There is already a repository at {0}", pathFound);

                    return 0;
                });
            });

            /// This command allow to show the directory and the file inside a path
            app.Command("files", c =>
            {

                c.Description = "Get files";
                var locationArgument = c.Argument("[location]",
                                   "Where the files should be located .");

                c.HelpOption("-?,|-h|--help");

                c.OnExecute(() =>
                {
                    var projectPath = locationArgument.Value != null && locationArgument.Value != "" ? locationArgument.Value : Directory.GetCurrentDirectory();
                    List<string> fileList = new List<string>();
                    if (File.Exists(projectPath))
                    {
                        ProcessFile(projectPath, fileList);
                    }
                    else if (Directory.Exists(projectPath))
                    {
                        FileTree ft = new FileTree();
                        ft.Nodes = new List<FileTree>();
                        GetNodes(projectPath, ft);
                        string json = JsonConvert.SerializeObject(ft, Formatting.Indented);
                        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "fileList.json"), json);
                        ProcessDirectory(projectPath, fileList);

                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid file or directory.", projectPath);
                    }

                    Console.ReadLine();
                    return 0;
                });
            });

            app.Execute(args);
            Console.ReadLine();
        }

        public static void GetNodes(string path, FileTree ft)
        {
            if (File.Exists(path))
            {
                ft = new FileTree(path);
            }
            else if (Directory.Exists(path))
            {
                GetFiles(path, ft);
                ft.Data = path;
                foreach (string item in Directory.GetDirectories(path))
                {

                    FileTree n = new FileTree();
                    n.Data = item;
                    n.Nodes = new List<FileTree>();
                    GetFiles(item, n);
                    ft.Nodes.Add(n);
                    GetNodes(item, ft);
                }
            }
        }

        public static void GetFiles(string path, FileTree ft)
        {
            foreach (string item in Directory.GetFiles(path))
            {
                ft.Nodes.Add(new FileTree(item));
            }
        }
        public static void ProcessDirectory(string targetDirectory, List<string> fileList)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName, fileList);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);

            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory, fileList);
        }
        public static List<string> ProcessFile(string path, List<string> fileList)
        {
            Console.WriteLine(Path.GetFileName(path));
            fileList.Add(Path.GetFileName(path));
            return fileList;
        }
    }

}
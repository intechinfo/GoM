using System;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using GoM.Core.Persistence;
using GoM.Core.GitExplorer;
using LibGit2Sharp;
using GoM.Core.Mutable;
using GoM.Core.FSAnalyzer;
using Microsoft.Extensions.FileProviders;
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
                app.ShowHelp("GoM");
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

                command.OnExecute(() =>
                {
                    int repos = repositoryOption.Values.Count;
                    int proj = projectOption.Values.Count;
                    int allProj = allProjectOption.Values.Count;
                    int branch = branchOption.Values.Count;

                    // add repository
                    if (repos > 0)
                    {
                        var projectPath = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : Directory.GetCurrentDirectory();
                        Communicator com = new Communicator(projectPath);
                        Console.WriteLine("The repository was added");
                    }
                    // add branch
                    else if (branch > 0)
                    {
                        var nameBranch = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : null;
                        try
                        {
                            Communicator com = new Communicator(Directory.GetCurrentDirectory());
                            var branches = com.getAllBranches();
                            bool isBranchExist = false;

                            foreach (var b in branches)
                            {
                                if (b.Name == nameBranch)
                                {
                                    isBranchExist = true;
                                }
                            }

                            if (isBranchExist) Console.WriteLine("This branch already exist");
                            else
                            {
                                using (Repository repo = com.loadRepository(com.Path))
                                {
                                    repo.CreateBranch(nameBranch);
                                    var allBranches = com.getAllBranches();
                                    Console.WriteLine("The branch " + nameBranch + " is created");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message + " : You must specify a name for your branch");
                        }
                    }
                    // add project
                    else if (proj > 0 && allProj == 0)
                    {

                    }
                    // add all project
                    else if (proj > 0 && allProj > 0)
                    {
                        Communicator com = new Communicator(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
                        ProjectFolderController projectFolderController = new ProjectFolderController();

                        foreach (var project in projectFolderController.Analyze(com.FileProvider).ToList())
                        {
                            Core.Mutable.Project p = new Core.Mutable.Project(project);
                            Core.Mutable.BasicProject basicProject = new Core.Mutable.BasicProject() { Path = project.Path, Details = p };
                            Console.WriteLine(p.Path);
                            Console.WriteLine("salute");
                        }
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



                command.OnExecute(() =>
                {
                    int repo = excludeRepoOPtion.Values.Count;
                    int branch = excludeBranchOPtion.Values.Count;
                    int allBranches = excludeAllBranchOPtion.Values.Count;
                    int project = excludeProjectOPtion.Values.Count;
                    int allProjects = excludeAllProjectshOPtion.Values.Count;

                    var path = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : Directory.GetCurrentDirectory();
                    var name = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : null;

                    // remove repo
                    if (repo > 0)
                    {
                        try
                        {
                            var removeRepo = Helpers.DeleteGitRepository(path);
                            if (removeRepo)
                            {
                                Console.WriteLine("Repository successfully removed.");
                            }
                            else
                            {
                                Console.WriteLine("Repository wasn't removed.");
                            }

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The path specified isn't valid.");
                            Console.WriteLine(e.Message);
                        }


                    }
                    // remove branch
                    else if (branch > 0)
                    {
                        Console.WriteLine("remove branch option");

                        Communicator c = new Communicator("B:/alexandrebabeu sur mon Mac/Desktop/IN'TECH/C#/GoM");
                        //Communicator c = new Communicator(Directory.GetCurrentDirectory());

                        var branches = c.getAllBranches();
                        foreach (var br in branches)
                        {
                            Console.WriteLine(br.Name);
                        }
                        Console.WriteLine(c.getAllBranches().First().Name);
                        foreach (var b in branches)
                        {
                            if (b.Name.Equals(name))
                            {
                                b.Details = null;
                                if (b.Details == null)
                                {
                                    Console.WriteLine("Branch details set to null.");
                                    try
                                    {
                                        branches.Remove(b);
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Remove failed " + e.Message);
                                    }

                                }
                            }
                        }
                    }
                    // remove project
                    else if (project > 0)
                    {

                    }
                    // remove all projects
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

            app.Command("refresh", command =>
            {
                command.Description = "";
                command.HelpOption("|-h|--help");
                CommandArgument projectLocationArgument = command.Argument("[location]", "");
                command.OnExecute(() =>
                {
                    var path = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : Directory.GetCurrentDirectory();

                    var projects = new ProjectFolderController().Analyze(new PhysicalFileProvider(path));

                    foreach (var proj in projects)
                    {

                        Console.WriteLine(proj);
                        Console.WriteLine("-ProjectPath: " + proj.Details.Path);
                        foreach (var tar in proj.Targets)
                        {
                            Console.WriteLine("--Target: " + tar.Name);
                            foreach (var depend in tar.Dependencies)
                            {
                                Console.WriteLine("--------: " + depend.Name + " | " + depend.Version);
                            }
                        }
                        Console.WriteLine("");
                    }

                    return 0;
                });
            });

            app.Command("fetch", c =>
            {
                c.Description = "Fetch updates on the remote server.";
                var repoPath = c.Argument("[repoPath]",
                                   "Where the repository should be located .");
                var branchName = c.Argument("[branchName]", "The name of the branch to fetch.");

                c.HelpOption("-h|--help");

                c.OnExecute(() =>
                {
                    var path = repoPath.Value != null && repoPath.Value != "" ? repoPath.Value : Directory.GetCurrentDirectory();
                    var name = branchName.Value != null && branchName.Value != "" ? branchName.Value : null;

                    Communicator remoteRepo = new Communicator(path);
                    //Communicator localRepo = new Communicator(Directory.GetCurrentDirectory());

                    var remoteBranches = remoteRepo.getAllBranches();

                    foreach (var b in remoteBranches)
                    {
                        if (b.Name.Equals(name))
                        {
                            Console.WriteLine("We can fetch the branch : {0}", name);
                            break;
                        }
                    }
                    return 0;
                });
            });

            /// This command allow to show the directory and the file inside a path
            app.Command("files", c =>
            {
                c.Description = "Get directories and files relative to a path";
                var locationArgument = c.Argument("[location]",
                                   "Where the files should be located .");
                c.HelpOption("|-h|--help");
                c.OnExecute(() =>
                {
                    var projectPath = locationArgument.Value != null && locationArgument.Value != "" ? locationArgument.Value : Directory.GetCurrentDirectory();
                    List<string> fileList = new List<string>();
                    if (File.Exists(projectPath))
                    {
                        ProcessFile(projectPath, fileList);
                    }
                    else if (Directory.Exists(projectPath) && Repository.IsValid(projectPath))
                    {

                        FileTree ft = new FileTree();
                        ft.Nodes = new List<FileTree>();
                        GetNodes(projectPath, ft);
                        string json = JsonConvert.SerializeObject(ft, Formatting.Indented);
                        //Console.WriteLine(json);
                        File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "fileList.json"), json);
                        ProcessDirectory(projectPath, fileList);

                    }
                    else
                    {
                        Console.WriteLine("{0} is not a valid file or directory.", projectPath);
                    }
                    return 0;
                });
            });
            try
            {
                app.Execute(args);
            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message + ",you must specify a valid option");
            }
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
                    GetChildren(item, n);
                }

            }
        }
        public static void GetChildren(string path, FileTree ft)
        {
            if (Directory.Exists(path))
            {
                foreach (string item in Directory.GetDirectories(path))
                {
                    FileTree n = new FileTree();
                    n.Data = item;
                    n.Nodes = new List<FileTree>();
                    GetFiles(item, n);
                    ft.Nodes.Add(n);
                    GetChildren(item, ft);
                }
            }
        }
        public static void GetFiles(string path, FileTree ft)
        {
            foreach (string item in Directory.GetFiles(path))
            {
                ft.Nodes.Add(new FileTree(Path.GetFileName(item)));
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
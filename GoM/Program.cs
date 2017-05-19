using System;
using Microsoft.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using GoM.Core.Persistence;
using LibGit2Sharp;
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
                    , CommandOptionType.MultipleValue);

                CommandOption projectOption = command.Option("-p|--project",
                    "Project to add to the repository",
                    CommandOptionType.MultipleValue);

                CommandOption allProjectOption = command.Option("-p -all|--project -all",
                   "Add all the projects to the repository",
                   CommandOptionType.MultipleValue);

                CommandOption branchOption = command.Option("-b|--branch",
                    "Add a branch to GoM",
                    CommandOptionType.MultipleValue);

                CommandArgument projectLocationArgument = command.Argument("[location]", "Where the projects should be located");

                command.OnExecute(() =>
                {
                    // To immplement 
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
                    CommandOptionType.MultipleValue);

                var excludeBranchOPtion = command.Option("-b|--branch",
                   "Exclude one branch from GoM",
                   CommandOptionType.MultipleValue);

                var excludeAllBranchOPtion = command.Option("-b -all|--branch -all",
                  "Exclude all branches from GoM",
                  CommandOptionType.MultipleValue);

                var excludeProjectOPtion = command.Option("-p|--project",
                  "Exclude one project from GoM",
                  CommandOptionType.MultipleValue);

                var excludeAllProjectshOPtion = command.Option("-p -all|--projects -all",
                  "Exclude all projects from GoM",
                  CommandOptionType.MultipleValue);

                command.OnExecute(() =>
                {
                    // To immplement 
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
                    var succes =  new Persistence().TryInit(myCurrentDirectory,out pathFound);

                    if (succes) Console.WriteLine("GoM repository has been correctly initialized");
                    else Console.WriteLine("GoM repository initialisation failed. There is already a repository at {0}",pathFound);

                    return 0;
                });
            });

            /// This command allow to show the directory and the file inside a path
            app.Command("files", c =>
            {
                c.Description = "Get directories and files relative to a path";
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

            app.Execute(args);
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
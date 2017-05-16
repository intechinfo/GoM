using GoM.Core.Mutable;
using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.CommandLine
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
                    ,CommandOptionType.MultipleValue);

                CommandOption projectOption = command.Option("-p|--project",
                    "Project to add to the repository",
                    CommandOptionType.MultipleValue);

                CommandOption allProjectOption = command.Option("-p -all|--project -all",
                   "Add all the projects to the repository",
                   CommandOptionType.MultipleValue);

                CommandOption branchOption = command.Option("-b|--branch",
                    "Add a branch to GoM",
                    CommandOptionType.MultipleValue);

                CommandArgument projectLocationArgument = command.Argument("[location]","Where the projects should be located");

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
                        Console.WriteLine(projectPath);
                        if (File.Exists(projectPath))
                        {
                            ProcessFile(projectPath);
                        }
                        else if (Directory.Exists(projectPath))
                        {
                            ProcessDirectory(projectPath);
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
            }
        
        public static void ProcessDirectory(string targetDirectory)
        {
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);

            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }
        public static void ProcessFile(string path)
        {
            Console.WriteLine(Path.GetFileName(path));
        }
    }
       
    }



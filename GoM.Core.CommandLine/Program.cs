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

                /*CommandOption excludeBranchOption = command.Option("-e -b| --exclude -branch",
                    "Exclude a branch to GoM",
                    CommandOptionType.MultipleValue);

                CommandOption excludeprojectOption = command.Option("-e -p| --exclude -project",
                   "Exclude a project from a repository",
                   CommandOptionType.MultipleValue);

                CommandOption excludeAllProjectOption = command.Option("-e -p -all| --exclude -project - all",
                  "Exclude all projects from the repository",
                  CommandOptionType.MultipleValue);*/

                CommandArgument projectLocationArgument = command.Argument("[location]","Where the projects should be located");

                command.OnExecute(() =>
                {
                    var location = projectLocationArgument.Values.Count() > 0 ? projectLocationArgument.Value : "no value";
                    //var projectPath = projectLocationArgument.Value != null && projectLocationArgument.Value != "" ? projectLocationArgument.Value : Directory.GetDirectories(location);
                    Console.WriteLine(location);
                    return 0;
                });
            });

            app.Command("attack", (command) =>
            {
                command.Description = "Instruct the ninja to go and attack!";
                command.HelpOption("-h|--help");

                var excludeOption = command.Option("-e|--exclude <exclusions>",
                               "Branch/Repository to exclude of the selection.",
                               CommandOptionType.MultipleValue);

                var screamOption = command.Option("-s|--scream",
                                       "Scream while attacking",
                                       CommandOptionType.NoValue);

                command.OnExecute(() =>
                {
                    var exclusions = excludeOption.Values;
                    var attacking = (new List<string>
                {
                    "dragons",
                    "badguys",
                    "civilians",
                    "animals"
                }).Where(x => !exclusions.Contains(x));

                    Console.Write("Ninja is attacking " + string.Join(", ", attacking));

                    if (screamOption.HasValue())
                    {
                        Console.Write(" while screaming");
                    }

                    Console.WriteLine();

                    return 0;
                });
             });

            /// Command list file
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



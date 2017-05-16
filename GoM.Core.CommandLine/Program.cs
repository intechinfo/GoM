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
            app.Name = "gom";
            app.HelpOption("-h|--help");

            app.OnExecute(() => {
                Console.WriteLine("Hello World!");
                return 0;
            });

            app.Command("hide", (command) =>
             {
                 command.Description = "Ceci est une description.";
                 command.HelpOption("-?,|-h|--help");

                 command.OnExecute(() =>
                 {
                     Console.WriteLine("");
                     return 0;
                 });
             });

            app.Command("add", (command) =>
            {
                command.Description = "Ceci est une description.";
                command.HelpOption("-h|--help");

                var locationArgument = command.Argument("[location]",
                                    "Where the ninja should hide.");

                command.OnExecute(() =>
                {
                    var location = locationArgument.Values.Count() > 0 ? locationArgument.Value : "under a turtle";
                    Console.WriteLine("Ninja is hidden " + location);
                    return 0;
                });
            });

            app.Command("attack", (command) =>
            {
                command.Description = "Instruct the ninja to go and attack!";
                command.HelpOption("-?|-h|--help");

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
            app.Command("files", c =>
            {
                
                c.Description = "Get files";
                var locationArgument = c.Argument("[location]",
                                   "Where the files should be located .");

               

            app.Command("files", c =>

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
        public static void ProcessFile(string path)
        {
            Console.WriteLine(Path.GetFileName(path));
        }
    }

}

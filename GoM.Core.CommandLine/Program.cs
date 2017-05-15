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
            /*if(args == null || args.Length == 0)
            {
                Array.Resize(ref args, args.Length + 1);
                args[args.Length - 1] = Console.ReadLine();
            }
            CommandLineApplication cmdLineApplication = new CommandLineApplication(false);

            cmdLineApplication.Command("Hello", (cmd) =>
            {
                cmd.Description = "Simple Hello World";
                cmd.HelpOption("-h");

                cmd.OnExecute(() =>
                {
                    Console.WriteLine("Hello World!");
                    return 0;
                });

            }, false);

            cmdLineApplication.Command("GoodBye", (cmd) =>
            {
                cmd.Description = "Simple GoodBye";
                cmd.HelpOption("-h");

                cmd.OnExecute(() =>
                {
                    Console.WriteLine("Goodbye!");
                    return 0;
                });

            }, false);

            //Help implementation
            cmdLineApplication.Command("h", (cmd) =>
            {
                cmd.Description = "Simple GoodBye";
                cmd.HelpOption("-h");

                cmd.OnExecute(() =>
                {
                    var cmds = cmdLineApplication.Commands;
                    foreach(var command in cmds)
                    {
                        Console.WriteLine(command.Name);
                    }
                   
                    return 0;
                });

            }, false);
            foreach (var cnt in args)
            {
                cmdLineApplication.Execute(cnt);
            }
            
            Console.ReadLine();*/
            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.Name = "gom";
            app.HelpOption("-?,|-h|--help");

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
                     Console.WriteLine("Bene Bene Bene");
                     return 0;
                 });
             });
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
            app.Command("add", (command) =>
            {
                command.Description = "Ceci est une description.";
                command.HelpOption("-?,|-h|--help");

                var locationArgument = command.Argument("[location]",
                                    "Where the ninja should hide.");

                command.OnExecute(() =>
                {
                    var location = locationArgument.Values.Count() > 0 ? locationArgument.Value : "under a turtle";
                    Console.WriteLine("Ninja is hidden " + location);
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

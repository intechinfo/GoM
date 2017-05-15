﻿using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.CommandLine
{
    class Program
    {
        static void Main(string[] args)
        {
<<<<<<< HEAD
            if(args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments");
=======
            /*if(args == null || args.Length == 0)
            {
                Array.Resize(ref args, args.Length + 1);
                args[args.Length - 1] = Console.ReadLine();
>>>>>>> b0d6c26eaf940e82dbeb74f07557fca3dcf38a1d
            }

            var app = new CommandLineApplication(throwOnUnexpectedArg: false);
            app.Name = "gom";
            app.HelpOption("-?,|-h|--help");

            app.OnExecute(() => {
                Console.WriteLine("Bienvenue sur GoM");
                return 0;
            });

            app.Command("Hello", (cmd) =>
            {
                cmd.Description = "Simple Hello World";

                cmd.OnExecute(() => 
                {
                    Console.WriteLine("Hello World!");
                    return 0;
                });

            }, false);

            app.Command("GoodBye", (cmd) =>
            {
                cmd.Description = "Simple GoodBye";

                cmd.OnExecute(() =>
                {
                    Console.WriteLine("Goodbye!");
                    return 0;
                });

            }, false);

<<<<<<< HEAD
            app.Execute(args);
            

            var count = app.Commands.Count;
            Console.ReadLine();
=======
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
>>>>>>> b0d6c26eaf940e82dbeb74f07557fca3dcf38a1d
           
        }
        }
}   

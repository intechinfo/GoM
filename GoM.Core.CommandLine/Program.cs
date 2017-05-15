using Microsoft.Extensions.CommandLineUtils;
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
            if(args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments");
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

            app.Execute(args);
            

            var count = app.Commands.Count;
            Console.ReadLine();
           
        }
        }
}   

using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.CommandLine
{
    class Program : CommandLineApplication
    {
<<<<<<< HEAD
        public static void Main(params string[] args)
        {
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

            cmdLineApplication.HelpOption("-x");

            cmdLineApplication.Execute("Hello");
            
=======
        static void Main(string[] args)
        {
            if(args == null || args.Length == 0)
            {
                Console.WriteLine("No arguments");
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
            foreach (var cnt in args)
            {
                cmdLineApplication.Execute(cnt);
            }
            
            Console.ReadLine();
           
        }
>>>>>>> 1244fbdb17d3eeff1f320f596b2d4549cc635553
        }
}   

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
            
        }
    }
}

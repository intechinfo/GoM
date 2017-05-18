using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoM.Core.GitExplorer;
namespace GoMConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            test();
        }

        public static void test()
        {
            Communicator communicator = new Communicator(@"C:\Users\Adams\Documents\visual studio 2017\Projects\GoM\GoMConsole\bin\Debug\repos\SGV-Net");

            List<BasicGitBranchDecorator> branches = communicator.getAllBranches();
          
            int count = branches.Count;

            foreach (var branch in branches)
            {
                Console.WriteLine(branch.Name);
                if (!branch.Details.Version.LastTag.Equals(null))
                {
                    Console.WriteLine(branch.Details.Version.LastTag.FullName);
                }
            }

            Console.WriteLine("Files : " + Environment.NewLine);

            foreach (var file in communicator.getFiles())
            {
                Console.WriteLine(file);
            }

            Console.WriteLine("Directories : " + Environment.NewLine);

            foreach (var directory in communicator.getFolders())
            {
                Console.WriteLine(directory);
            }

            Console.WriteLine(Environment.NewLine);

            Console.WriteLine(count);

            Console.ReadLine();
        }
    }
}

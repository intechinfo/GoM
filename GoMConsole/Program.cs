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
            Communicator communicator = new Communicator("https://github.com/intechinfo/GoM.git");

            var branches = communicator.getAllBranches();
            int count = branches.Count;

            foreach(var branch in branches)
            {
                Console.WriteLine(branch.Name);
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

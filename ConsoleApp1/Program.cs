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
        [STAThread]
        static void Main(string[] args)
        {
            Communicator communicator = new Communicator("https://github.com/bmgm/Simple.git");
            communicator.loadRepository("https://github.com/SimpleGitVersion/SGV-Net.git");

            var aaa = communicator.getBasicGitRepository();

            var branches = communicator.getAllBranches();

            int count = branches.Count;

            foreach (var branch in branches)
            {
                Console.WriteLine(branch.Name);
                if (branch.Details.Version.LastTag != null)
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

            Console.WriteLine("Nombre de branches: " + count);

            foreach(var repo in Communicator.getAllStoredRepositoriesNames())
            {
                Console.WriteLine("Repo : " + repo);
            }

            Console.ReadLine();

        }

    }
}

using System;
using System.IO;
using LibGit2Sharp;

namespace GoM.Core.Git
{
    public class Commands
    {
        public static string sourceRepos = "repos";
        public static void Git_clone(string sourceUrl)
        {

            //Parse repository name
            string[] sourceUrl_exploded = sourceUrl.Split('/');
            string repoFullName = sourceUrl_exploded[sourceUrl_exploded.Length - 1];
            string[] repoNameFullName_exploded = repoFullName.Split('.');
            string repoName = repoNameFullName_exploded[0];

            string path = sourceRepos + "/" + repoName;

            bool fileExist = File.Exists(path);

            if(fileExist)
            {
                Console.WriteLine("[Core] repository already downloaded.");
                return;
            }

            Console.WriteLine(path.ToString());
            try
            {
                Console.WriteLine("[Core] downloading repository..");

                //Clone repository
                Repository.Clone(sourceUrl, path);
                Console.WriteLine("[Core] Git repo " + sourceUrl + " is stored in "+ path + " folder.");
                Console.ReadLine();
            }
            catch(LibGit2SharpException exception)
            {
                Console.WriteLine(exception.Message);
                Console.ReadLine();
            }
        }
    }
}

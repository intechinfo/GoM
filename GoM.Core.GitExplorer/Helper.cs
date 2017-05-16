using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GoM.Core.GitExplorer
{
    public class Helpers
    {
        public enum UrlShape { Fullname, Name };

        public static string ParseUrl(string path, UrlShape shape)
        {
            //Parse repository name
            string[] sourceUrl_exploded = path.Split('/');
            string repoFullName = sourceUrl_exploded[sourceUrl_exploded.Length - 1];
            string[] repoFullName_exploded = repoFullName.Split('.');
            string repoName = repoFullName_exploded[0];

            if (shape == UrlShape.Fullname) return repoFullName;
            if (shape == UrlShape.Name) return repoName;

            return null;
        }

        public static string ParsePath(string path)
        {
            //Parse repository name
            string[] sourceUrl_exploded = path.Split('/');
            string repoName = sourceUrl_exploded[sourceUrl_exploded.Length - 1];

            return repoName;
        }

        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }
    }
}
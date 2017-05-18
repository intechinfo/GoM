using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace GoM.Core.GitExplorer
{
    public static class Helpers
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

        public static List<string> getAllFilesInDirectory(string target_dir, string searchPattern = "*")
        {
            return Directory.GetFiles(target_dir, searchPattern, SearchOption.AllDirectories).ToList();
        }

        public static List<string> getAllFoldersInDirectory(string target_dir, string searchPattern = "*")
        {
            return Directory.GetDirectories(target_dir, searchPattern, SearchOption.AllDirectories).ToList();
        }

        public static bool IsWebSource(string source)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(source);
            request.Method = WebRequestMethods.Http.Head;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch (WebException webEx)
            {
                return false;
            }

        }

        /// <summary>
        /// Check if source is valid
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool SourceIsValid(string source)
        {
            if (IsWebSource(source)) return true;
            else if(Directory.Exists(source)) return true;
            return false;
        }
    }
}
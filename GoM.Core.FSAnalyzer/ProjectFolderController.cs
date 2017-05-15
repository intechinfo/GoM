using GoM.Core.Abstractions;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GoM.Core.FSAnalyzer
{
    public class ProjectFolderController : IProjectFolderController
    {
        public IReadOnlyCollection<IProject> Analyze(string path)
        {
            List<IProject> projects = new List<IProject>();

            //Check the .git on the path
            PhysicalFileProvider rootPath = new PhysicalFileProvider(path);
            if (rootPath.GetDirectoryContents(".git").Exists)
            {
                IDirectoryContents directories = rootPath.GetDirectoryContents("");
                IEnumerator<IFileInfo> fileInfos = directories.GetEnumerator();

            }
            //On each project call specialize handler with PhysicalFileProvider

            //If true, add in collection
            //Return IProject collection
            IProject project = new Project();
            projects.Add(project);
            IReadOnlyCollection<IProject> readOnlyProjects = new ReadOnlyCollection<IProject>(projects);
            return readOnlyProjects;
        }
    }
}

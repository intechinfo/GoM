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
        public IReadOnlyCollection<IProject> Analyze(IFileProvider gitFileProvider)
        {
            List<IProject> projects = new List<IProject>();

            IDirectoryContents directories = gitFileProvider.GetDirectoryContents("");
            foreach (IFileInfo fileInfo in directories)
            {
                if (fileInfo.IsDirectory)
                {
                    // On each project call specialize handler with PhysicalFileProvider
                    ProjectFolderHandler projectHandler = new ProjectFolderHandler(gitFileProvider, fileInfo.PhysicalPath);
                    IProjectFolderHandler specializedProjectHandler = projectHandler.Sniff();
                    if (specializedProjectHandler != null)
                    {
                        // If true, add in collection
                        // Return IProject collection
                        IProject project = specializedProjectHandler.Read();
                        projects.Add(project);
                    }
                }
            }

            IReadOnlyCollection<IProject> readOnlyProjects = new ReadOnlyCollection<IProject>(projects);
            return readOnlyProjects;
          }
    }
}

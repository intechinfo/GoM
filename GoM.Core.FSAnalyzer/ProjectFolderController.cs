using GoM.Core.Abstractions;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace GoM.Core.FSAnalyzer
{
    public class ProjectFolderController : IProjectFolderController
    {
        public IReadOnlyCollection<IProject> Analyze(IFileProvider gitFileProvider)
        {
            return DoAnalyze(gitFileProvider, "");
        }

        private IReadOnlyCollection<IProject> DoAnalyze(IFileProvider provider, string path)
        {
            List<IProject> projects = new List<IProject>();
            var directories = provider.GetDirectoryContents(path).Where(x => x.Name != ".git" && x.IsDirectory);

            foreach (IFileInfo fileInfo in directories)
            {
                var relativePath = Path.Combine(path, fileInfo.Name);

                // On each project call specialize handler with PhysicalFileProvider
                ProjectFolderHandler projectHandler = new ProjectFolderHandler(provider, relativePath);
                IProjectFolderHandler specializedProjectHandler = projectHandler.Sniff();

                if (specializedProjectHandler != null)
                {
                    // If true, add in collection
                    // Return IProject collection
                    projects.Add(specializedProjectHandler.Read());
                }
                else
                {
                    projects.AddRange(DoAnalyze(provider, relativePath));
                }
            }
            return projects;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.DocFx;
using System.IO;
using Cake.DocFx.Metadata;
using Cake.DocFx.Build;
using Cake.Core.IO;

namespace GoM.Doc
{
    public class DocMaker
    {
        protected string repoPath;
        protected DocFxBuildSettings docFxBuildSettings;
        protected DocFxMetadataSettings docFxMetadataSettings;

        public string RepoPath {
            get { return repoPath; }
            protected set { repoPath = value; }
        }

        public DocFxBuildSettings DocFxBuildSettings {
            get { return docFxBuildSettings; }
            protected set { docFxBuildSettings = value; }
        }

        public DocFxMetadataSettings DocFxMetadataSettings {
            get { return docFxMetadataSettings; }
            protected set { docFxMetadataSettings = value; }
        }

        public DocMaker(string repoPath)
        {
            this.repoPath = repoPath;

            docFxBuildSettings = new DocFxBuildSettings();
            docFxMetadataSettings = new DocFxMetadataSettings();

            docFxBuildSettings.Serve = true;
            List<FilePath> paths = new List<FilePath>();
            paths.Add(new FilePath(repoPath));

            docFxMetadataSettings.Projects = paths;

            //DocFxMetadataSettings metadataSetting = new DocFxMetadataSettings();

            //metadataSetting.Projects = repoPath;
        }

        public void generateDoc()
        {
            
            //Cake.DocFx.Init.DocFxInitSettings

        }
        
    }
}

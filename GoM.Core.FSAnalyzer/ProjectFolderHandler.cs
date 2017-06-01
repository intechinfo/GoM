using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using GoM.Core.Abstractions;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public class ProjectFolderHandler : BaseProjectFolderHandler
    {
        public ProjectFolderHandler(IFileProvider provider, string currentPathFolder) : base(provider, currentPathFolder)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            if (Files.FirstOrDefault(e => Regex.IsMatch(e.Name, ".csproj$")) != null)
                return new CsharpProjectHandler(FileProvider, CurrentPathFolder).Sniff();

            if( HasFile( "package.json" ) )
            {
                return new JsProjectHandler(FileProvider, CurrentPathFolder).Sniff();
            }
            if( HasFile( "setup.py" ) )
            {
                return new PythonProjectHandler( FileProvider, CurrentPathFolder).Sniff();
            }
            if (HasFile("composer.json"))
            {
                return new PhpProjectHandler( FileProvider, CurrentPathFolder).Sniff();
            }
            return null;
        }

        public override IProject Read()
        {
            return null;
        }
    }
}

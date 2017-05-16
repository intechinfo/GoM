using Microsoft.Extensions.FileProviders.Physical;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.GitFileProvider
{

    public class GitFilesWatcher : PhysicalFilesWatcher
    {
        readonly string _path;
        readonly FileSystemWatcher _FSWatcher;
        readonly bool _pollForChanges;

        public GitFilesWatcher(string path, FileSystemWatcher fileSystemWatcher, bool pollForChanges) : base(path, fileSystemWatcher, pollForChanges)
        {
            _path = path;
            _FSWatcher = fileSystemWatcher;
            _pollForChanges = pollForChanges;
            _FSWatcher.Changed += OnChanged;
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            
            Console.WriteLine("Changed");
        }
    }
}

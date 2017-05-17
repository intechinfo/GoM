using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoM.GitFileProvider
{

    public class GitFilesWatcher
    {
        readonly string _path;
        readonly FileSystemWatcher _FSWatcher;
        readonly bool _pollForChanges;
        readonly List<ChangeTokenWrapper> _fileMonitoredPool;
        readonly GitFileProvider _gitFileProvider;

        public GitFilesWatcher(string path, FileSystemWatcher fileSystemWatcher, bool pollForChanges, GitFileProvider gitFileProvider)
        {
            _path = path;
            _FSWatcher = fileSystemWatcher;
            _pollForChanges = pollForChanges;
            _fileMonitoredPool = new List<ChangeTokenWrapper>();
            _gitFileProvider = gitFileProvider;
            _FSWatcher.Changed += MyOnChanged;
            _FSWatcher.Created += MyOnChanged;
            _FSWatcher.Deleted += MyOnChanged;
            _FSWatcher.EnableRaisingEvents = true;
        }
        
        /// <summary>
        /// We know that the Git folder has changed, need to find every tracked file and see if they have changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyOnChanged(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Inside onchanged");
            foreach (ChangeTokenWrapper wrap in _fileMonitoredPool)
            {
                Task.Run(() =>
                {
                    try
                    {
                        wrap.ChangeToken.TokenSource.Cancel();
                    }
                    catch
                    {
                    }
                });
            }
        }

        private ChangeTokenWrapper MyCreateFileChangeToken(string filter)
        {
            ChangeTokenInfo tokenInfo;
           
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationChangeToken = new CancellationChangeToken(cancellationTokenSource.Token);
            tokenInfo = new ChangeTokenInfo(cancellationTokenSource, cancellationChangeToken);

            ChangeTokenWrapper changeTokenWrap = new ChangeTokenWrapper(tokenInfo, filter);
            return changeTokenWrap;
        }

        public IChangeToken MonitorFile(string filter)
        {
            ChangeTokenWrapper tokenWrap = MyCreateFileChangeToken(filter);
            _fileMonitoredPool.Add(tokenWrap);
            return tokenWrap.ChangeToken.ChangeToken;
        }

        private struct ChangeTokenWrapper
            {
                public ChangeTokenInfo ChangeToken { get; }
                public string Path { get; }

                public ChangeTokenWrapper(ChangeTokenInfo changeToken, string path)  
                {
                ChangeToken = changeToken;
                Path = path;
                }
            }

        private struct ChangeTokenInfo
        {
            public ChangeTokenInfo(
               CancellationTokenSource tokenSource,
               CancellationChangeToken changeToken)
            {
                TokenSource = tokenSource;
                ChangeToken = changeToken;
            }

            public CancellationTokenSource TokenSource { get; }

            public CancellationChangeToken ChangeToken { get; }
            
        }
    }
}

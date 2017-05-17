using LibGit2Sharp;
using Microsoft.Extensions.FileProviders;
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
            foreach (ChangeTokenWrapper wrap in _fileMonitoredPool)
            {
                Task.Run(() =>
                {
                
                    try
                    {
                        IFileInfo fileInfo = _gitFileProvider.GetFileInfo(wrap.Path);
                        FileInfoFile fileInfoFile = fileInfo as FileInfoFile;
                        if (fileInfoFile.File.Sha != wrap.FileBlob.Sha)
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
            IFileInfo fileInfo = _gitFileProvider.GetFileInfo(filter);
            if (!fileInfo.Exists)
                return new ChangeTokenWrapper(new ChangeTokenInfo(null, null), null, default(DateTimeOffset), null); 

            ChangeTokenInfo tokenInfo;
           
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationChangeToken = new CancellationChangeToken(cancellationTokenSource.Token);
            tokenInfo = new ChangeTokenInfo(cancellationTokenSource, cancellationChangeToken);

            FileInfoFile fileInfoFile = fileInfo as FileInfoFile;
            ChangeTokenWrapper changeTokenWrap = new ChangeTokenWrapper(tokenInfo, filter, fileInfo.LastModified, fileInfoFile.File);
            return changeTokenWrap;
        }

        public IChangeToken MonitorFile(string filter)
        {
            ChangeTokenWrapper tokenWrap = MyCreateFileChangeToken(filter);
            if (tokenWrap.Path == null)
                return NullChangeToken.Singleton;

            _fileMonitoredPool.Add(tokenWrap);
            return tokenWrap.ChangeToken.ChangeToken;
        }

        private struct ChangeTokenWrapper
            {
                public ChangeTokenInfo ChangeToken { get; }
                public string Path { get; }
                public DateTimeOffset LastModificationTime { get; }
                public Blob FileBlob;
                public ChangeTokenWrapper(ChangeTokenInfo changeToken, string path, DateTimeOffset lastModificationTime, Blob fileBlob)  
                {
                    ChangeToken = changeToken;
                    Path = path;
                    LastModificationTime = lastModificationTime;
                    FileBlob = fileBlob;
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

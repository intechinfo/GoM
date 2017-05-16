using System;

namespace GoM.Core.Immutable
{
    public class BasicGitRepository : IBasicGitRepository
    {
        private readonly string _path;

        private readonly Uri _url;

        private readonly GitRepository _details;

        private BasicGitRepository(string path, Uri url, GitRepository details = null)
        {
            _path = path ?? throw new ArgumentException("path must not be null");
            _url = url ?? throw new ArgumentException("url must not be null");
            _details = details;
        }

        public string Path => _path;

        public Uri Url => _url;

        public GitRepository Details => _details;

        IGitRepository IBasicGitRepository.Details => _details;

        public BasicGitRepository WithAll(string path = null, Uri url = null, GitRepository details = null)
        {
            if (_path == path && _url == url && _details == details)
            {
                return this;
            }
            path = path ?? _path;
            url = url ?? _url;
            details = details ?? _details;

            return new BasicGitRepository(path, url, details);
        }
    }
}
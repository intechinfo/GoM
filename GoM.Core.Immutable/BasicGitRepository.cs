using System;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class BasicGitRepository : IBasicGitRepository
    {
        private BasicGitRepository(string path, Uri url, IGitRepository details = null)
        {
            Path = path ?? throw new ArgumentException(nameof(path));
            Url = url ?? throw new ArgumentException(nameof(url));
            Details = details != null ? GitRepository.Create(details) : null;
        }

        private BasicGitRepository(IBasicGitRepository basicGitRepository)
        {
            Debug.Assert(basicGitRepository is BasicGitRepository);
            Path = basicGitRepository.Path ?? throw new ArgumentException(nameof(basicGitRepository.Path));
            Url = basicGitRepository.Url ?? throw new ArgumentException(nameof(basicGitRepository.Url));
            Details = basicGitRepository.Details != null ? GitRepository.Create(basicGitRepository.Details) : null;
        }

        public string Path { get; }

        public Uri Url { get; }

        public GitRepository Details { get; }

        IGitRepository IBasicGitRepository.Details => Details;

        public BasicGitRepository WithAll(string path = null, Uri url = null, GitRepository details = null)
        {
            if (Path == path && Url == url && Details == details)
            {
                return this;
            }
            path = path ?? Path;
            url = url ?? Url;
            details = details ?? Details;

            return new BasicGitRepository(path, url, details);
        }

        public static BasicGitRepository Create(string path, Uri url)
        {
            return new BasicGitRepository(path, url);
        }

        public static BasicGitRepository Create(IGitRepository details)
        {
            return new BasicGitRepository(details.Path, details.Url, details);
        }

        public static BasicGitRepository Create(IBasicGitRepository basicGitRepository) => basicGitRepository as BasicGitRepository ?? new BasicGitRepository(basicGitRepository);
    }
}
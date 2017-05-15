using GoM.Core; using System;

namespace GoM.Core.Persistence
{
    public class BasicGitRepository : IBasicGitRepository
    {
        public string Path { get; }

        public Uri Url { get; }

        public GitRepository Details { get; }

        IGitRepository IBasicGitRepository.Details => Details;


        public BasicGitRepository(string path, Uri url, GitRepository details)
        {
            Path    = path;
            Url     = url;
            Details = details;
        }
    }
}
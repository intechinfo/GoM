using System;

namespace GoM.Core.Mutable
{
    public class BasicGitRepository : IBasicGitRepository
    {
        public BasicGitRepository()
        {
        }

        /// <summary>
        /// Creates a Mutable BasicGitRepository from an existing IBasicGitRepository, ie from an Immutable BasicGitRepository
        /// </summary>
        /// <param name="repo"></param>
        public BasicGitRepository(IBasicGitRepository repo)
        {
            Path = repo.Path;
            Details = (GitRepository)repo.Details;
        }

        public string Path { get; set; }

        public Uri Url { get; set; }

        public GitRepository Details { get; set; }

        IGitRepository IBasicGitRepository.Details => Details;
    }
}
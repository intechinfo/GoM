using System;

namespace GoM.Core.Mutable
{
    public class BasicGitRepository : IBasicGitRepository
    {
        public string Path { get; set; }

        public Uri Url { get; set; }

        public GitRepository Details { get; set; }

        IGitRepository IBasicGitRepository.Details => Details;
}
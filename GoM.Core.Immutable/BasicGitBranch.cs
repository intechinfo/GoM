using System;
using System.Diagnostics;

namespace GoM.Core.Immutable
{
    public class BasicGitBranch : IBasicGitBranch
    {
        BasicGitBranch(IBasicGitBranch basicGitBranch)
        {
            Debug.Assert(!(basicGitBranch is BasicGitBranch));
            Name = basicGitBranch.Name ?? throw new ArgumentException(nameof(basicGitBranch.Name));
            Details = basicGitBranch.Details != null ? GitBranch.Create(basicGitBranch.Details) : null;
        }
        BasicGitBranch(string name, GitBranch details = null)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Details = details;
        }

        public string Name { get; }

        public GitBranch Details { get; }

        IGitBranch IBasicGitBranch.Details => Details;

        public static BasicGitBranch Create(string name, GitBranch details = null) => new BasicGitBranch(name, details);

        public static BasicGitBranch Create(IBasicGitBranch gitBranch) => gitBranch as BasicGitBranch ?? new BasicGitBranch(gitBranch);
    }
}
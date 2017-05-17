using System;

namespace GoM.Core.Immutable
{
    public class BasicGitBranch : IBasicGitBranch
    {
        BasicGitBranch(string name, GitBranch details)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Details = Details != null ? GitBranch.Create() : null;
        }

        BasicGitBranch(IBasicGitBranch basicGitBranch)
        {
            Name = basicGitBranch.Name ?? throw new ArgumentException(nameof(basicGitBranch.Name));

        }

        public string Name { get; }

        public GitBranch Details { get; }

        IGitBranch IBasicGitBranch.Details => Details;

    }
}
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace GoM.Core.Immutable
{
    public class GitBranch : IGitBranch
    {
        GitBranch(string name, IBranchVersionInfo version = null, ImmutableList<BasicProject> projects = null)
        {
            Name = name ?? throw new ArgumentException(nameof(name));
            Version = version != null ? BranchVersionInfo.Create(version) : null;
            if (projects != null) Projects = projects;

            // Check duplicates in projects
            if (CheckDuplicates(projects)) throw new ArgumentException($"Duplicates found in {nameof(projects)}");
        }

        GitBranch(IGitBranch branch)
        {
            Debug.Assert(!(branch is GitBranch));
            Projects = (ImmutableList<BasicProject>)branch.Projects ?? ImmutableList.Create<BasicProject>();
            Version = branch.Version != null ? BranchVersionInfo.Create(branch.Version) : null;

            // Check duplicates in projects
            if (CheckDuplicates(Projects)) throw new ArgumentException($"Duplicates found in {nameof(Projects)}");
        }

        public ImmutableList<BasicProject> Projects { get; } = ImmutableList.Create<BasicProject>();

        public BranchVersionInfo Version { get; }

        public static GitBranch Create(IGitBranch details) => details as GitBranch ?? new GitBranch(details);

        public string Name { get; }

        public static GitBranch Create(string name, BranchVersionInfo version = null)
        {
            return new GitBranch(name, version);
        }
        bool CheckDuplicates(ImmutableList<BasicProject> projects)
        {
            return projects.Distinct(
                            EqualityComparerGenerator.CreateEqualityComparer<BasicProject>(
                                (x, y) => x.Path == y.Path, x => x.Path.GetHashCode())
                                ).Count() < projects.Count;
        }

        public GitBranch Details => this;

        IBranchVersionInfo IGitBranch.Version => Version;

        IGitBranch IBasicGitBranch.Details => Details;

        IReadOnlyCollection<IBasicProject> IGitBranch.Projects => Projects;
    }
}
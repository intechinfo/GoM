using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Immutable
{
    public static class ImmutableGoMExtensions
    {
        public static GoMContext ToImmutable(this IGoMContext context) => GoMContext.Create(context);

        public static BasicGitRepository ToImmutable(this IBasicGitRepository repository) => BasicGitRepository.Create(repository);

        public static GitRepository ToImmutable(this IGitRepository repository) => GitRepository.Create(repository);

        public static BasicGitBranch ToImmutable(this IBasicGitBranch branch) => BasicGitBranch.Create(branch);

        public static GitBranch ToImmutable(this IGitBranch branch) => GitBranch.Create(branch);

        public static BranchVersionInfo ToImmutable(this IBranchVersionInfo version) => BranchVersionInfo.Create(version);

        public static VersionTag ToImmutable(this IVersionTag tag) => VersionTag.Create(tag);

        public static BasicProject ToImmutable(this IBasicProject project) => BasicProject.Create(project);

        public static Project ToImmutable(this IProject project) => Project.Create(project);

        public static Target ToImmutable(this ITarget target) => Target.Create(target);

        public static TargetDependency ToImmutable(this ITargetDependency dependency) => TargetDependency.Create(dependency);

        public static PackageFeed ToImmutable(this IPackageFeed feed) => PackageFeed.Create(feed);

        public static PackageInstance ToImmutable(this IPackageInstance package) => PackageInstance.Create(package);

    }
}

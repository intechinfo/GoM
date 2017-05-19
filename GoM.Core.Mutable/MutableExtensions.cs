using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Mutable

{
    public static class MutableGoMExtensions
    {
        public static GoMContext ToMutable(this IGoMContext context) => new GoMContext(context);

        public static BasicGitRepository ToMutable(this IBasicGitRepository repository) => new BasicGitRepository(repository);

        public static GitRepository ToMutable(this IGitRepository repository) => new GitRepository(repository);

        public static BasicGitBranch ToMutable(this IBasicGitBranch branch) => new BasicGitBranch(branch);

        public static GitBranch ToMutable(this IGitBranch branch) => new GitBranch(branch);

        public static BranchVersionInfo ToMutable(this IBranchVersionInfo version) => new BranchVersionInfo(version);

        public static VersionTag ToMutable(this IVersionTag tag) => new VersionTag(tag);

        public static BasicProject ToMutable(this IBasicProject project) => new BasicProject(project);

        public static Project ToMutable(this IProject project) => new Project(project);

        public static Target ToMutable(this ITarget target) => new Target(target);

        public static TargetDependency ToMutable(this ITargetDependency dependency) => new TargetDependency(dependency);

        public static PackageFeed ToMutable(this IPackageFeed feed) => new PackageFeed(feed);

        public static PackageInstance ToMutable(this IPackageInstance package) => new PackageInstance(package);

    }
}

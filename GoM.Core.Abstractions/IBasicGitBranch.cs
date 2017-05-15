namespace GoM.Core
{
    public interface IBasicGitBranch
    {
        string Name { get; }

        IGitBranch Details { get; }
    }
}
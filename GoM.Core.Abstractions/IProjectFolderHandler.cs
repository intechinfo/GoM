namespace GoM.Core.Abstractions
{
    public interface IProjectFolderHandler
    {
        bool Sniff(string path);

        IProject Read(string path);
    }
}

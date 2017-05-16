using Microsoft.Extensions.FileProviders;

namespace GoM.Core.Abstractions
{
    public interface IProjectFolderHandler
    {
        IProjectFolderHandler Sniff();
        IFileProvider FileProvider { get; }
        IProject Read();
    }
}

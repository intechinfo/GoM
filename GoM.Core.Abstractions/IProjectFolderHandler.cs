using Microsoft.Extensions.FileProviders;

namespace GoM.Core.Abstractions
{
    public interface IProjectFolderHandler
    {
        bool Sniff();
        IFileProvider FileProvider { get; }
        IProject Read();
    }
}

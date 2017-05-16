using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public class PackagesConfigHandler : BaseProjectFolderHandler
    {
        public PackagesConfigHandler(IFileProvider provider) : base(provider)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GoM.Core.Abstractions;
using GoM.Core.FSAnalyzer.Utils;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public class CsharpProjectHandler : BaseProjectFolderHandler
    {
        public CsharpProjectHandler(IFileProvider provider) : base(provider)
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            if (HasFile("packages.config"))
            {
                return new PackagesConfigHandler(FileProvider).Sniff();
            }
            else
            {
                return new CsProjHandler(FileProvider).Sniff();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GoM.Core.Abstractions;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer
{
    public class CsharpProjectHandler : BaseProjectFolderHandler
    {
        public CsharpProjectHandler(IFileProvider provider) : base(provider)
        {
        }

        public override IProject Read()
        {
            return base.Read();
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

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.FileProviders;
using GoM.Core.Abstractions;

namespace GoM.Core.FSAnalyzer
{
    public class PhpProjectHandler: BaseProjectFolderHandler
    {
        public PhpProjectHandler(IFileProvider provider) 
            : base( provider )
        {
        }

        public override IProjectFolderHandler Sniff()
        {
            return this;
        }

        public override IProject Read()
        {
            return base.Read();
        }
    }
}

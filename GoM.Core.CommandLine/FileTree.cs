using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.CommandLine
{
    class FileTree
    {
        public FileTree()
        { }
        public FileTree(string fileName)
        {  
            Data = fileName;
            Nodes = new List<FileTree>();
        }

        public List<FileTree> Nodes { get; set; }

        public string Data { get; set; }
    }
}

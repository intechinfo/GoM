using GoM.Core;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace GoM.Persistence
{
    class Persistence : IPersistence
    {
        string FolderName { get; }
        string FileName { get; }

        public Persistence(string folderName=".gom", string fileName="context")
        {
            FolderName = folderName;
            FileName = fileName;
        }


        public IGoMContext Load ()
        {
            throw new NotImplementedException();
        }

        public void Save ( IGoMContext ctx)
        {
            if ( ctx == null ) throw new ArgumentNullException();

        }

    }
}

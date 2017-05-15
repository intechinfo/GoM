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


        public IGomContext Load ()
        {
            throw new NotImplementedException();
        }

        public void Save ( IGomContext ctx)
        {
            if ( ctx == null ) throw new ArgumentNullException();

            Directory.CreateDirectory( Path.Combine( ctx.RootPath, FolderName ) );

            using ( var stream = File.Create( Path.Combine( ctx.RootPath, FolderName, FileName ) ) )
            {
                IFormatter formatter = new BinaryFormatter();

                formatter.Serialize( stream, ctx );
            }

        }




    }
}

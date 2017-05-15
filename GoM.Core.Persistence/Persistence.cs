using GoM.Core;
using GoM.Core.Mutable;
using System;
using System.IO;
using System.Xml.Linq;

namespace GoM.Persistence
{
    public static class Helper
    {
        public static XElement ToXML ( this PackageInstance _this )
        {
            XElement element = new XElement(typeof(PackageInstance).Name);
            element.SetAttributeValue( nameof( _this.Version ), _this.Version );
            element.SetAttributeValue( nameof( _this.Name ), _this.Name );
            return element;
        }

        public static XElement ToXML(this PackageFeed _this)
        {
            XElement element = new XElement(typeof(PackageFeed).Name);
            element.SetAttributeValue( nameof( _this.Url ), _this.Url );
            foreach ( PackageInstance package in _this.Packages )
            {
                element.Add( package.ToXML() );
            }
            return element;
        }

    }
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


        #region Extensions

        

        #endregion

    }
}

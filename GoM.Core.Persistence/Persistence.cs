using GoM.Core;
using GoM.Core.Mutable;
using System;
using System.IO;
using System.Xml.Linq;

namespace GoM.Persistence
{
    public static class Helper
    {
        public static XElement ToXML ( this IPackageInstance _this )
        {
            XElement element = new XElement(typeof(PackageInstance).Name);
            element.SetAttributeValue( nameof( _this.Version ), _this.Version );
            element.SetAttributeValue( nameof( _this.Name ), _this.Name );
            return element;
        }

        public static XElement ToXML(this IPackageFeed _this)
        {
            XElement element = new XElement(typeof(PackageFeed).Name);
            element.SetAttributeValue( nameof( _this.Url ), _this.Url );
            foreach ( PackageInstance package in _this.Packages )
            {
                element.Add( package.ToXML() );
            }
            return element;
        }

        public static XElement ToXML ( this IGoMContext _this )
        {
            XElement element = new XElement(typeof(IGoMContext).Name);
            element.SetAttributeValue( nameof( _this.RootPath ), _this.RootPath );
            foreach ( var t in _this.Repositories ) element.Add( t.ToXML() );
            foreach ( var t in _this.Feeds ) element.Add( t.ToXML() );
            return element;
        }

        public static XElement ToXML ( this IProject _this )
        {
            XElement element = new XElement(typeof(IGoMContext).Name);
            element.SetAttributeValue( nameof( _this.Path ), _this.Path );
            foreach ( var t in _this.Targets ) element.Add( t.ToXML() );
            return element;
        }

        public static XElement ToXML ( this ITargetDependency _this )
        {
            XElement element = new XElement(typeof(IGoMContext).Name);
            element.SetAttributeValue( nameof( _this.Name ), _this.Name );
            element.SetAttributeValue( nameof( _this.Version ), _this.Version );
            return element;
        }

        public static XElement ToXML ( this IVersionTag _this )
        {
            XElement element = new XElement(typeof(IGoMContext).Name);
            element.SetAttributeValue( nameof( _this.FullName ), _this.FullName );
            
            return element;
        }
        public static XElement ToXML ( this ITarget _this )
        {
            XElement element = new XElement(typeof(IGoMContext).Name);
            element.SetAttributeValue( nameof( _this.Name ), _this.Name );
            foreach ( var t in _this.Dependencies ) element.Add( t.ToXML() );

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

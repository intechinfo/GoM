using GoM.Core.Mutable;
using GoM.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;
using System.IO;

namespace GoM.Core.Persistence.Tests
{
    public class PersistenceTests
    {
        [Fact]
        public void test_runner_working()
        {
            Assert.True( true );
        }

        [Fact]
        public void first_xml_try_on_package_instance()
        {

            PackageInstance pi = new PackageInstance();
            pi.Name = "SDL";
            pi.Version = "2.0";

            XDocument doc = new XDocument();
            doc.Add();

            XElement element = new XElement(typeof(PackageInstance).Name);
            element.SetAttributeValue( nameof( pi.Version), pi.Version);
            element.SetAttributeValue( nameof(pi.Name), pi.Name );
            doc.Add( element );

            var s = doc.ToString();
            Console.WriteLine( s );
            Assert.True( s == "<PackageInstance Version=\"2.0\" Name=\"SDL\" />" );
        }

        [Fact]
        public void xml_works_using_extensions()
        {
            PackageInstance pi = new PackageInstance() { Name = "SDL", Version="2.0" };
            PackageInstance pi2 = new PackageInstance() { Name = "SDLbit", Version="2.0" };
            PackageInstance pi3 = new PackageInstance() { Name = "SDLou", Version="2.0" };

            PackageFeed pf = new PackageFeed()
            {
                Url = new Uri("http://www.google.fr")
            };
            pf.Packages.Add( pi );
            pf.Packages.Add( pi2 );
            pf.Packages.Add( pi3 );


            XDocument doc = new XDocument();
            doc.Add( pf.ToXML() );


            var s = doc.ToString();
            Console.WriteLine( s );

        }


        [Fact]
        public void first_xml_try_create_multiple_node ()
        {
            PackageInstance pi = new PackageInstance() { Name = "SDL", Version="2.0" };
            PackageInstance pi2 = new PackageInstance() { Name = "SDLbit", Version="2.0" };
            PackageInstance pi3 = new PackageInstance() { Name = "SDLou", Version="2.0" };

            PackageFeed pf = new PackageFeed()
            {
                Url = new Uri("http://www.google.fr")
            };
            pf.Packages.Add( pi );
            pf.Packages.Add( pi2 );
            pf.Packages.Add( pi3 );


            XElement element = new XElement(typeof(PackageFeed).Name);
            element.SetAttributeValue( nameof( pf.Url ), pf.Url );
            foreach(PackageInstance package in pf.Packages)
            {
                element.Add( package.ToXML() );
            }


            XDocument doc = new XDocument();
            doc.Add(element);


            var s = doc.ToString();
            Console.WriteLine( s );

        }

        [Fact]
        public void read_version()
        {
            Assert.True(true);
        }

        [Fact]
        public void try_read_first_node()
        {
            var data = File.ReadAllText( Path.Combine( ".", ".gom", "name" ) );
            XDocument doc = XDocument.Parse( data );
            IGoMContext context = new GoMContext();
        }


    }
}

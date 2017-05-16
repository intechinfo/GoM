using GoM.Core.Mutable;
using GoM.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;
using System.IO;
using System.Linq;

namespace GoM.Core.Persistence.Tests
{
    public class PersistenceTests
    {
        [Fact]
        public void test_runner_working()
        {
            Assert.True(true);
        }

        [Fact]
        public void first_xml_try_on_package_instance()
        {

            PackageInstance pi = new PackageInstance("SDL", "2.0");

            XDocument doc = new XDocument();
            doc.Add();

            XElement element = new XElement(typeof(PackageInstance).Name);
            element.SetAttributeValue(nameof(pi.Version), pi.Version);
            element.SetAttributeValue(nameof(pi.Name), pi.Name);
            doc.Add(element);

            var s = doc.ToString();
            Console.WriteLine(s);
            Assert.True(s == "<PackageInstance Version=\"2.0\" Name=\"SDL\" />");
        }

        [Fact]
        public void xml_works_using_extensions()
        {
            PackageInstance pi = new PackageInstance("SDL", "2.0");
            PackageInstance pi2 = new PackageInstance("SDL2", "2.0");
            PackageInstance pi3 = new PackageInstance("SDL3", "2.0");

            PackageFeed pf = new PackageFeed(new Uri("http://www.google.fr"));
            pf.Packages.Add(pi);
            pf.Packages.Add(pi2);
            pf.Packages.Add(pi3);


            XDocument doc = new XDocument();
            doc.Add(pf.ToXML());


            var s = doc.ToString();
            Console.WriteLine(s);

        }


        [Fact]
        public void first_xml_try_create_multiple_node()
        {
            PackageInstance pi = new PackageInstance("SDL", "2.0");
            PackageInstance pi2 = new PackageInstance("SDL2", "2.02");
            PackageInstance pi3 = new PackageInstance("SDL3", "2.03");

            PackageFeed pf = new PackageFeed(new Uri("http://www.google.fr"));
            pf.Packages.Add(pi);
            pf.Packages.Add(pi2);
            pf.Packages.Add(pi3);


            XElement element = new XElement(typeof(PackageFeed).Name);
            element.SetAttributeValue(nameof(pf.Url), pf.Url);
            foreach (PackageInstance package in pf.Packages)
            {
                element.Add(package.ToXML());
            }


            XDocument doc = new XDocument();
            doc.Add(element);


            var s = doc.ToString();
            Console.WriteLine(s);

        }

        [Fact]
        public void read_version()
        {
            Assert.True(true);
        }

        [Fact]
        public void try_read_first_node()
        {
            var data = File.ReadAllText(Path.Combine(".", ".gom", "name"));
            XDocument doc = XDocument.Parse(data);
            IGoMContext context = new GoMContext(doc.Root);
        }

        [Fact]
        public void load_the_gom_context_from_gom_file()
        {
            string rootPath = ".";
            string folderName = ".gom";
            string fileName = "";

            var data = File.ReadAllText(Path.Combine(rootPath, folderName, fileName));

            XDocument doc = XDocument.Parse(data);





            //var t = from c in doc.FirstNode select "";

            doc.FirstNode.Remove();
            var ctx = new GoMContext(doc.Root);

            Assert.True (ctx.RootPath == ".");
            foreach (var repositories in ctx.Repositories)
            {
                Assert.True(repositories.Path == ".");
                Assert.True(repositories.Url == new Uri("http://github.com/projet.git"));
                Assert.True(repositories.Details.Path == "repo");
                Assert.True(repositories.Details.Url == new Uri(""));
                foreach( var y in  repositories.Details.Branches)
                {
                    Assert.True(y.Name == "projet");
                }
            }
            foreach (var feed in ctx.Feeds)
            {
                Assert.True(feed.Url == new Uri ("http://google.fr"));
                foreach( var package in feed.Packages)
                {
                    Assert.True(package.Name == "bite");
                    Assert.True(package.Version == "v1.0.0");
                }
            }
            

            Console.WriteLine("");

        }


    }
}

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
        string TuPath( [System.Runtime.CompilerServices.CallerFilePath] string s = "" )
        {
            return Path.GetDirectoryName( s );
        }

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

            doc.FirstNode.Remove();
            var ctx = new GoMContext(doc.Root);

            Assert.True(ctx.RootPath == ".");

            Assert.True(ctx.Repositories[0].Path == ".");
            Assert.True(ctx.Repositories[0].Url == new Uri("http://github.com/projet.git"));
            Assert.True(ctx.Repositories[0].Details.Path == "repo");
            Assert.True(ctx.Repositories[0].Details.Url == new Uri(""));

            Assert.True(ctx.Repositories[0].Details.Branches[0].Name == "projet");



            Assert.True(ctx.Feeds[0].Url == new Uri("http://google.fr"));

            Assert.True(ctx.Feeds[0].Packages[0].Name == "bite");
            Assert.True(ctx.Feeds[0].Packages[0].Version == "v1.0.0");





        }

        [Fact]
        public void test_generatefake()
        {

            var ctx = GenerateFakeContextHelper();

            Console.Write(ctx);

        }
        [Fact]
        public void try_save()
        {
            IGoMContext completeFake = GenerateFakeContextHelper();
            Persistence p = new Persistence();
            p.Save( completeFake );
        }

        public IGoMContext GenerateFakeContextHelper()
        {
            // OK
            var package1 = new Mutable.PackageInstance();
            package1.Version = "1.0.0";
            package1.Name = "bite";

            // OK
            var package2 = new Mutable.PackageInstance();
            package2.Version = "2.0.0";
            package2.Name = "p�nis";

            // OK
            var package3 = new Mutable.PackageInstance();
            package3.Version = "1.5.0";
            package3.Name = "chibre";

            // OK
            var feed1 = new Mutable.PackageFeed();
            feed1.Url = new Uri("http://www.google.fr");
            feed1.Packages.Add(package1);
            feed1.Packages.Add(package2);
            feed1.Packages.Add(package3);

            // OK
            var feed2 = new Mutable.PackageFeed();
            feed2.Url = new Uri("http://www.google.fr");

            // OK
            var targetDependency = new Mutable.TargetDependency();
            targetDependency.Name = "dependency1";
            targetDependency.Version = "1.0.0";

            // OK
            var target1 = new Mutable.Target();
            target1.Name = "target1";
            target1.Dependencies.Add( targetDependency );

            // OK
            var target2 = new Mutable.Target();
            target2.Name = "target2";

            // OK
            var target3 = new Mutable.Target();
            target2.Name = "target3";

            // OK
            var project = new Mutable.Project();
            project.Path = "./fakeproject1/";
            project.Targets.Add(target1);
            project.Targets.Add(target2);

            // OK
            var project2 = new Mutable.Project();
            project.Path = "./fakeprojet2/";
            project.Targets.Add(target3);

            // OK
            var project3 = new Mutable.Project();
            project.Path = "./fakeproject3/";

            // OK
            var versionTag = new Mutable.VersionTag();
            versionTag.FullName = "v1.0.0";

            // OK
            var branchVersion = new Mutable.BranchVersionInfo();
            branchVersion.LastTag = versionTag;
            branchVersion.LastTagDepth = 0;

            // OK
            var realGitbranch = new Mutable.GitBranch();
            realGitbranch.Details = realGitbranch;
            realGitbranch.Name = "BiteOfTheDead";

            realGitbranch.Version = branchVersion;

            realGitbranch.Projects.Add(project);
            realGitbranch.Projects.Add(project2);
            realGitbranch.Projects.Add(project3);

            // OK
            var basicbranch = new Mutable.BasicGitBranch();
            basicbranch.Details = realGitbranch;
            basicbranch.Name = "develop";

            // OK
            var basicbranch2 = new Mutable.BasicGitBranch();
            basicbranch2.Details = null;
            basicbranch2.Name = "Cubado";

            // OK
            var realgitrepo = new Mutable.GitRepository();
            realgitrepo.Url = new Uri("http://www.google.fr");
            realgitrepo.Path = "/usr/developpement/GoM/";
            realgitrepo.Details = realgitrepo;
            realgitrepo.Branches.Add(basicbranch);
            realgitrepo.Branches.Add(basicbranch2);

            // OK
            var basicrepo2 = new Mutable.BasicGitRepository();
            basicrepo2.Details = realgitrepo;
            basicrepo2.Path = "/usr/developpement/lolilol";
            basicrepo2.Url = new Uri("http://www.google.fr");

            // OK
            var basicrepo = new Mutable.BasicGitRepository();
            basicrepo.Path = "/usr/developpement/mdr";
            basicrepo.Url = new Uri("http://www.google.fr");
            basicbranch.Details = null;

            // OK
            Mutable.GoMContext ctx = new Mutable.GoMContext();
            ctx.RootPath = TuPath();
            ctx.Feeds.Add(feed1);
            ctx.Feeds.Add(feed2);
            ctx.Repositories.Add(basicrepo2);
            ctx.Repositories.Add(basicrepo);

            return ctx;
        }



    }
}
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
        string TuPath([System.Runtime.CompilerServices.CallerFilePath] string s = "")
        {
            return Path.GetDirectoryName(s);
        }

        [Fact]
        public void test_integration_with_given_batch()
        {
            var path = new DirectoryInfo(TuPath());

            // run bat if Dev folder doesn't exists
            if ( path.Parent.GetDirectories().FirstOrDefault( ( e ) => e.Name == "Dev" ) == null )
            {
                if (path.Parent.GetFiles().FirstOrDefault( e => e.Name == "CreateSampleRoot.bat" ) == null)
                {
                    throw new Exception( "CreateSapleRoot.bat is needed" );
                }
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = path.Parent.GetFiles().FirstOrDefault( e => e.Name == "CreateSampleRoot.bat" ).FullName;
                proc.StartInfo.WorkingDirectory = path.Parent.FullName;
                proc.Start();
                proc.WaitForExit();
                // TODO error management for CI
            }
            // get dev dir & clean in all directory if exists
            var devFolder = path.Parent.GetDirectories().First( e => e.Name == "Dev");
            CleanGomPersistenceInAllFolder( devFolder );
            // It's testing time !
            Persistence p = new Persistence();
            string output;
            // First init works
            Assert.True(p.TryInit( devFolder.FullName, out output ));

            // Second init doesn't work and return devfolder path
            Assert.False(p.TryInit( devFolder.FullName, out output ));
            Assert.True( output == devFolder.FullName );

            // Go childs folder and retry init
            Assert.False( p.TryInit( devFolder.GetDirectories()[1].FullName, out output ) );
            Assert.True( output == devFolder.FullName );
            Assert.False( p.TryInit( devFolder.GetDirectories() [1].GetDirectories()[1].FullName, out output ) );
            Assert.True( output == devFolder.FullName );

            var ctx = p.Load( devFolder.FullName );
            Assert.True(ctx.Repositories.ToList()[0].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-AspNet-Projects")
                .GetDirectories().First(el => el.Name == "CK-AspNet" ).FullName );
            Assert.True( ctx.Repositories.ToList() [1].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-AspNet-Projects" )
                .GetDirectories().First( el => el.Name == "CK-AspNet-Auth" ).FullName );
            Assert.True( ctx.Repositories.ToList() [2].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-Core-Projects" )
                .GetDirectories().First( el => el.Name == "CK-ActivityMonitor" ).FullName );
            Assert.True( ctx.Repositories.ToList() [3].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-Core-Projects" )
                .GetDirectories().First( el => el.Name == "CK-Auth-Abstractions" ).FullName );
            Assert.True( ctx.Repositories.ToList() [4].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-Core-Projects" )
                .GetDirectories().First( el => el.Name == "CK-Core" ).FullName );
            Assert.True( ctx.Repositories.ToList() [5].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-Core-Projects" )
                .GetDirectories().First( el => el.Name == "CK-Monitoring" ).FullName );
            Assert.True( ctx.Repositories.ToList() [6].Path == devFolder
                .GetDirectories().First( el => el.Name == "CK-Core-Projects" )
                .GetDirectories().First( el => el.Name == "CK-Reflection" ).FullName );
            Assert.True( ctx.Repositories.ToList() [7].Path == devFolder
               .GetDirectories().First( el => el.Name == "CK-Core-Projects" )
               .GetDirectories().First( el => el.Name == "CK-Text" ).FullName );
            Assert.True( ctx.Repositories.ToList() [8].Path == devFolder
               .GetDirectories().First( el => el.Name == "CK-Database-Projects" )
               .GetDirectories().First( el => el.Name == "CK-Database" ).FullName );
            Assert.True( ctx.Repositories.ToList() [9].Path == devFolder
               .GetDirectories().First( el => el.Name == "CK-Database-Projects" )
               .GetDirectories().First( el => el.Name == "CK-DB" ).FullName );
            Assert.True( ctx.Repositories.ToList() [10].Path == devFolder
               .GetDirectories().First( el => el.Name == "CK-Database-Projects" )
               .GetDirectories().First( el => el.Name == "CK-Setup-Dependency" ).FullName );
            Assert.True( ctx.Repositories.ToList() [11].Path == devFolder
               .GetDirectories().First( el => el.Name == "CK-Database-Projects" )
               .GetDirectories().First( el => el.Name == "CK-SqlServer-Parser" ).FullName );
            Assert.True( ctx.Repositories.ToList() [12].Path == devFolder
               .GetDirectories().First( el => el.Name == "CK-Database-Projects" )
               .GetDirectories().First( el => el.Name == "CK-SqlServer-Parser-Model" ).FullName );
            Assert.True( ctx.Repositories.ToList() [13].Path == devFolder
               .GetDirectories().First( el => el.Name == "NUnit-GUI" ).FullName );
            Assert.True( ctx.Repositories.ToList() [14].Path == devFolder
               .GetDirectories().First( el => el.Name == "Yodii" ).FullName );

        }

        private void CleanGomPersistenceInAllFolder ( DirectoryInfo devFolder )
        {
            Persistence p = new Persistence();
            foreach(var path in p.SearchFolderRecursive( devFolder, ".gom" ))
            {
                Directory.Delete( Path.Combine(path, ".gom"), true );
            }
        }

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
            doc.Add(pf.ToXml());


            var s = doc.ToString();
            Console.WriteLine(s);

        }


        public void CleanGoMPersistence()
        {
            // Delete .gom folder
            var path = Path.Combine(TuPath(), ".gom");
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }
        [Fact]
        public void load_the_gom_context_from_gom_file()
        {
            try_save();

            string rootPath = TuPath();

            var p = new Persistence();
            GoMContext ctx = (GoMContext)p.Load(rootPath);

#region Repo 1
            Assert.True(ctx.Repositories[1].Path == "/usr/developpement/mdr");
            Assert.True(ctx.Repositories[1].Details == null);
#endregion

#region Repo 2
            Assert.True(ctx.Repositories[0].Path == "/usr/developpement/lolilol");
            Assert.True(ctx.Repositories[0].Details.Url == new Uri("http://www.google.fr"));
            Assert.True(ctx.Repositories[0].Details.Path == "/usr/developpement/GoM/");

#region Branch 1
            Assert.True(ctx.Repositories[0].Details.Branches[0].Name == "develop");

#region Projet 1
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[0].Path == "fakebasicproject1" );
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.ToList()[0].Name == "target1");
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.ToList()[0].Dependencies.ToList()[0].Name == "dependency1");
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.ToList()[0].Dependencies.ToList()[0].Version == "1.0.0");

            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[0].Details.Targets.ToList()[1].Name == "target2");
#endregion

#region Projet 2
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[1].Path == "fakebasicproject2" );
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[1].Details.Targets.ToList()[0].Name == "target3");
#endregion

#region Projet 3
            Assert.True(ctx.Repositories[0].Details.Branches[0].Details.Projects[2].Path == "fakebasicproject3");
#endregion

#region Projet 4
            Assert.True( ctx.Repositories [0].Details.Branches [0].Details.Projects [3].Path == "fakebasicproject4" );
            Assert.True( ctx.Repositories [0].Details.Branches [0].Details.Projects [3].Details == null );
#endregion

#endregion

#region Branch 2
            Assert.True(ctx.Repositories[0].Details.Branches[1].Name == "Cubado");
            Assert.True(ctx.Repositories[0].Details.Branches[1].Details == null);
#endregion

#endregion

#region Feed 1
            Assert.True(ctx.Feeds[0].Url == new Uri("http://www.google.fr"));

            // Packages 1, 2 et 3
            Assert.True(ctx.Feeds[0].Packages[0].Version == "1.0.0");
            Assert.True(ctx.Feeds[0].Packages[0].Name == "ovgr");

            Assert.True(ctx.Feeds[0].Packages[1].Version == "2.0.0");
            Assert.True(ctx.Feeds[0].Packages[1].Name == "cravf");

            Assert.True(ctx.Feeds[0].Packages[2].Version == "1.5.0");
            Assert.True(ctx.Feeds[0].Packages[2].Name == "puvoer");
#endregion

#region Feed 2
            Assert.True(ctx.Feeds[1].Url == new Uri("http://www.google.com"));
#endregion

            CleanGoMPersistence();
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
            p.Save(completeFake);
        }

        public IGoMContext GenerateFakeContextHelper()
        {
            // OK
            var package1 = new Mutable.PackageInstance();
            package1.Version = "1.0.0";
            package1.Name = "ovgr";

            // OK
            var package2 = new Mutable.PackageInstance();
            package2.Version = "2.0.0";
            package2.Name = "cravf";

            // OK
            var package3 = new Mutable.PackageInstance();
            package3.Version = "1.5.0";
            package3.Name = "puvoer";

            // OK
            var feed1 = new Mutable.PackageFeed();
            feed1.Url = new Uri("http://www.google.fr");
            feed1.Packages.Add(package1);
            feed1.Packages.Add(package2);
            feed1.Packages.Add(package3);

            // OK
            var feed2 = new Mutable.PackageFeed();
            feed2.Url = new Uri("http://www.google.com");

            // OK
            var targetDependency = new Mutable.TargetDependency();
            targetDependency.Name = "dependency1";
            targetDependency.Version = "1.0.0";

            var targetDependency2 = new Mutable.TargetDependency();
            targetDependency2.Name = "dependency2";
            targetDependency2.Version = "2.0.0";

            var targetDependency3 = new Mutable.TargetDependency();
            targetDependency3.Name = "dependency3";
            targetDependency3.Version = "3.0.0";

            var targetDependency4 = new Mutable.TargetDependency();
            targetDependency4.Name = "dependency4";
            targetDependency4.Version = "4.0.0";

            var targetDependency5 = new Mutable.TargetDependency();
            targetDependency5.Name = "dependency5";
            targetDependency5.Version = "5.0.0";

            // OK
            var target1 = new Mutable.Target();
            target1.Name = "target1";
            target1.Dependencies.Add(targetDependency);
            target1.Dependencies.Add( targetDependency2 );

            // OK
            var target2 = new Mutable.Target();
            target2.Name = "target2";
            target2.Dependencies.Add( targetDependency3 );


            // OK
            var target3 = new Mutable.Target();
            target3.Name = "target3";
            target3.Dependencies.Add( targetDependency4 );


            var target4 = new Mutable.Target();
            target4.Name = "target4";
            target4.Dependencies.Add( targetDependency5 );


            // OK
            var project = new Mutable.Project();
            project.Path = "./fakeproject1/";
            project.Targets.Add(target1);
            project.Targets.Add(target2);

            // OK
            var project2 = new Mutable.Project();
            project2.Path = "./fakeprojet2/";
            project2.Targets.Add(target3);

            var project3 = new Mutable.Project();
            project3.Path = "./fakeprojet3/";
            project3.Targets.Add( target4 );

            // OK
            var basicproject1 = new Mutable.BasicProject();
            basicproject1.Path = "fakebasicproject1";
            basicproject1.Details = project;

            var basicproject2 = new Mutable.BasicProject();
            basicproject2.Path = "fakebasicproject2";
            basicproject2.Details = project2;

            var basicproject3 = new Mutable.BasicProject();
            basicproject3.Path = "fakebasicproject3";
            basicproject3.Details = project3;

            var basicproject4 = new Mutable.BasicProject();
            basicproject4.Path = "fakebasicproject4";
            basicproject4.Details = null;

            // OK
            var versionTag = new Mutable.VersionTag();
            versionTag.FullName = "v1.0.0";

            // OK
            var branchVersion = new Mutable.BranchVersionInfo();
            branchVersion.LastTag = versionTag;
            branchVersion.LastTagDepth = 0;

            // OK
            var realGitbranch = new Mutable.GitBranch();
            //realGitbranch.Details = realGitbranch;
            realGitbranch.Name = "ovgrOfTheDead";

            realGitbranch.Version = branchVersion;

            realGitbranch.Projects.Add(basicproject1);
            realGitbranch.Projects.Add(basicproject2);
            realGitbranch.Projects.Add(basicproject3);
            realGitbranch.Projects.Add(basicproject4);

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
            //realgitrepo.Details = realgitrepo;
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
            basicrepo.Details = null;

            // OK
            Mutable.GoMContext ctx = new Mutable.GoMContext();
            ctx.RootPath = TuPath();
            ctx.Feeds.Add(feed1);
            ctx.Feeds.Add(feed2);
            ctx.Repositories.Add(basicrepo2);
            ctx.Repositories.Add(basicrepo);

            return ctx;
        }

        [Fact]
        public void try_init_works()
        {
            CleanGoMPersistence();
            Persistence p = new Persistence();
            string outPath;

            Assert.True(p.TryInit(TuPath(), out outPath));
            Assert.True(outPath == string.Empty);

            Assert.False(p.TryInit(TuPath(), out outPath));
            Assert.True(outPath == TuPath());

            Assert.False(p.TryInit(TuPath(), out outPath));
            Assert.True(outPath == TuPath());

            CleanGoMPersistence();
            Assert.True(p.TryInit(TuPath(), out outPath));
            Assert.True(outPath == string.Empty);

            Assert.False(p.TryInit(TuPath(), out outPath));
            Assert.True(outPath == TuPath());

        }

    }
}

using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class PackageConfigParser : BaseConfigParser
    {
        public PackageConfigParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<Target> Read()
        {
            List<Target> target = new List<Target>();
            var physicalPath = Source.PhysicalPath;
            XDocument xml = new XDocument();
            FileStream xmlStream = new FileStream(physicalPath, FileMode.Open);
            var result = XDocument.Load(xmlStream);
            if (result.Root != null)
            {
                var pac = (from c in result.Root.Elements("package")
                    select c).ToArray();
                var ta = new Target();

                for (var i = 0; i < pac.Count(); i++)
                {
                    var id = pac[i].Attribute("id") != null ? pac[i].Attribute("id").Value : "null";
                    var version = pac[i].Attribute("version") != null ? pac[i].Attribute("version").Value : "null";
                    var targetFramework = pac[i].Attribute("targetFramework") != null ? pac[i].Attribute("targetFramework").Value : "null";

                    ta = new Target()
                    {
                        Name = targetFramework
                    };
                    ta.Dependencies.Add(new TargetDependency()
                    {
                        Name = id,
                        Version = version
                    });
                    target.Add(ta);
                }
            }
            return target;
        }
    }
}

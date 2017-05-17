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
            var physicalPath = Source.PhysicalPath;
            FileStream xmlStream = new FileStream(physicalPath, FileMode.Open);
            var result = XDocument.Load(xmlStream);
            var dict = new Dictionary<string, List<TargetDependency>>();
            if (result.Root != null)
            {
                var pac = (from c in result.Root.Elements("package")
                    select c).ToArray();

                for (var i = 0; i < pac.Count(); i++)
                {
                    var id = pac[i].Attribute("id") != null ? pac[i].Attribute("id").Value : "null";
                    var version = pac[i].Attribute("version") != null ? pac[i].Attribute("version").Value : "null";
                    var targetFramework = pac[i].Attribute("targetFramework") != null ? pac[i].Attribute("targetFramework").Value : "null";

                    if (!dict.ContainsKey(targetFramework))
                    {
                        dict.Add(targetFramework, new List<TargetDependency>());
                    }
                    dict[targetFramework].Add(new TargetDependency()
                    {
                        Name = id,
                        Version = version
                    });
                }
            }
            foreach (var kvp in dict)
            {
                var target = new Target() {Name = kvp.Key};
                target.Dependencies.AddRange(kvp.Value);
                yield return target;
            }
        }
    }
}

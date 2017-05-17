using System;
using System.Collections.Generic;
using Microsoft.Extensions.FileProviders;
using System.Xml.Linq;
using System.Linq;
using GoM.Core.Mutable;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class CsProjParser : BaseConfigParser
    {
        public CsProjParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<Target> Read()
        {
            List<Target> target = new List<Target>();
            var physicalPath = Source.PhysicalPath;
            XDocument xmldoc = XDocument.Load(physicalPath);
            XNamespace msbuild = xmldoc.Root.GetDefaultNamespace().NamespaceName;
            var ReferenceList = xmldoc.Descendants(msbuild + "Reference").ToArray();
            var ReferenceListOld = xmldoc.Descendants(msbuild + "PackageReference").ToArray();
            var ta = new Target();

            var TargetFrameworkVersionList = xmldoc.Descendants(msbuild + "TargetFrameworkVersion").ToArray();

            for (int i = 0; i < TargetFrameworkVersionList.Length; i++)
            {
                string targetFrameworkVersion = TargetFrameworkVersionList[i] != null ? TargetFrameworkVersionList[i].Value : string.Empty;

                ta = new Target()
                {
                    Name = targetFrameworkVersion,
                };

                target.Add(ta);
            }

            if (ReferenceList.Length > 0)
            {
                for (int i = 0; i < ReferenceList.Length; i++)
                {
                    string include = ReferenceList[i] != null ? ReferenceList[i].Attribute("Include").Value : string.Empty;

                    ta.Dependencies.Add(new TargetDependency()
                    {
                        Name = "",
                        Version = include
                    });
                }
            }
            else
            {
                for (int i = 0; i < ReferenceListOld.Length; i++)
                {
                    string include = ReferenceListOld[i] != null ? ReferenceListOld[i].Attribute("Include").Value : string.Empty;
                    string version = ReferenceListOld[i] != null ? ReferenceListOld[i].Element("Version").Value : string.Empty;
                    ta.Dependencies.Add(new TargetDependency()
                    {
                        Name = include,
                        Version = version
                    });
                }
            }
            
            return target;
        }
    }
}

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
            if (xmldoc.Root != null)
            {
                XNamespace msbuild = xmldoc.Root.GetDefaultNamespace().NamespaceName;
                var referenceList = xmldoc.Descendants(msbuild + "Reference").ToArray();
                var referenceListOld = xmldoc.Descendants(msbuild + "PackageReference").ToArray();
                var ta = new Target();

                var targetFrameworkVersionList = xmldoc.Descendants(msbuild + "TargetFrameworkVersion").ToArray();

                for (int i = 0; i < targetFrameworkVersionList.Length; i++)
                {
                    string targetFrameworkVersion = targetFrameworkVersionList[i] != null ? targetFrameworkVersionList[i].Value : string.Empty;

                    ta = new Target()
                    {
                        Name = targetFrameworkVersion,
                    };

                    target.Add(ta);
                }

                if (referenceList.Length > 0)
                {
                    for (int i = 0; i < referenceList.Length; i++)
                    {
                        string include = referenceList[i] != null ? referenceList[i].Attribute("Include").Value : string.Empty;
                        string[] words = include.Split(',');
                        
                        ta.Dependencies.Add(new TargetDependency()
                        {
                            Name = words[0],
                            Version = words.Length > 1 ? words[1]: ""
                        });
                    }
                }
                else
                {
                    for (int i = 0; i < referenceListOld.Length; i++)
                    {
                        string include = referenceListOld[i] != null ? referenceListOld[i].Attribute("Include").Value : string.Empty;
                        string version = referenceListOld[i] != null ? referenceListOld[i].Element("Version").Value : string.Empty;
                        ta.Dependencies.Add(new TargetDependency()
                        {
                            Name = include,
                            Version = version
                        });
                    }
                }
            }

            return target;
        }
    }
}

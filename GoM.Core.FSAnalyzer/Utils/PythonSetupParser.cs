using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer.Utils
{
    public class PythonSetupParser : BaseConfigParser
    {
        public PythonSetupParser(IFileInfo file) : base(file)
        {
        }

        public override IEnumerable<Target> Read()
        {
            string content = ReadFileContent();

            int idx = content.IndexOf("install_requires", StringComparison.Ordinal);
            if (idx == -1) yield break;
            var start = content.IndexOf('[', idx);
            for (int i = start; i <= content.Length; i++)
            {
                if (content[i] == ']')
                {
                    string substr = content.Substring(start + 1, i - start - 1);
                    var dependencies = Regex.Replace(substr, @"\s+", string.Empty).Split(new []{','}, StringSplitOptions.RemoveEmptyEntries);
                    var targets = 
                        from x in dependencies
                        let vIdx = x.IndexOfAny(new[] {'<', '>', '='})
                        let name = x.Substring(0, vIdx == -1 ? x.Length : vIdx).Trim('\'')
                        select new TargetDependency()
                        {
                            Name = name,
                            Version = vIdx == -1 ? "" : x.Substring(vIdx).Trim('\'')
                        };
                    var target = new Target();
                    target.Dependencies.AddRange(targets);
                    yield return target;
                    break;
                }
            }
        }
    }
}

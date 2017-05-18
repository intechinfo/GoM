using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GoM.Core.Mutable;
using Microsoft.Extensions.FileProviders;

namespace GoM.Core.FSAnalyzer.Utils
{
    public abstract class BaseConfigParser
    {
        public IFileInfo Source { get; }

        protected BaseConfigParser(IFileInfo file)
        {
            Source = file;
        }

        public string ReadFileContent()
        {
            var stream = Source.CreateReadStream();
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public virtual IEnumerable<Target> Read()
        {
            throw new NotImplementedException();
        }
    }
}

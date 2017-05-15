using GoM.Core;
using GoM.Feeds.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoM.Feeds 
{
    internal class PypiFeedReader : NpmFeedReader
    {
        public override Task<IEnumerable<IPackageInstance>> GetAllVersions(string name)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<ITarget>> GetDependencies(string name, string version)
        {
            throw new NotImplementedException();
        }
    }
}
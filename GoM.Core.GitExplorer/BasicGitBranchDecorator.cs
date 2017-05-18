using GoM.Core.Mutable;
using GoM.GitFileProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoM.Core.GitExplorer
{
    public class BasicGitBranchDecorator : BasicGitBranch, IDisposable
    {
        BasicGitBranch _gitBranch;
        RepositoryWrapper _repoWrap;
        public BasicGitBranchDecorator(BasicGitBranch gitBranch, RepositoryWrapper repoWrap)
        {
            _gitBranch = gitBranch;
            _repoWrap = repoWrap;
            repoWrap.StreamWrapperCount++;
            Name = gitBranch.Name;
            Details = gitBranch.Details;
        }

        public void Dispose()
        {
            _repoWrap.StreamWrapperCount--;
            _repoWrap.Dispose();
        }
    }
}

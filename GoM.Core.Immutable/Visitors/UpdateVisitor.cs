using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoM.Core.Immutable.Visitors
{
    public class DetailRepositoryVisitor : Visitor
    {
        private GitRepository _detailed;

        public DetailRepositoryVisitor(GitRepository detailed)
        {
            _detailed = detailed ?? throw new ArgumentNullException(nameof(detailed));
        }

        public override GoMContext Visit(GoMContext c)
        {
            return base.Visit(c);
        }

        public override BasicGitRepository Visit(BasicGitRepository basicRepository)
        {
            basicRepository = basicRepository.Details == _detailed ? basicRepository : BasicGitRepository.Create(_detailed);
            return base.Visit(basicRepository);
        }

        public override GitRepository Visit(GitRepository repository)
        {

            return base.Visit(repository);
        }
    }
}

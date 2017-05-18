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
            _detailed = detailed;
        }

        public override GoMContext Visit(GoMContext c)
        {
            if (c.Repositories.SingleOrDefault(rep => rep.Details == _detailed) != null)
            {

            }

            return base.Visit(c);
        }

        public override BasicGitRepository Visit(BasicGitRepository basicRepository)
        {
            //basicRepository = BasicGitRepository.Create(ba)
            return base.Visit(basicRepository);
        }

        public override GitRepository Visit(GitRepository repository)
        {

            return base.Visit(repository);
        }
    }
}

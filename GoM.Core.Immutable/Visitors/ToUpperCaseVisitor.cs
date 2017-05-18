using System;
using System.Linq;

namespace GoM.Core.Immutable.Visitors
{
    public class ToUppercaseVisitor : Visitor
    {
        readonly string _pattern;

        public ToUppercaseVisitor(string pattern)
        {
            _pattern = pattern;
        }

        public override GoMContext Visit(GoMContext c)
        {
            if (!c.RootPath.All(x => Char.IsUpper(x)) && c.RootPath.Contains(_pattern))
            {
                c = c.WithAll(c.RootPath.ToUpperInvariant(), c.Repositories, c.Feeds);
            }
            return base.Visit(c);
        }

        public override BasicGitRepository Visit(BasicGitRepository r)
        {
            if (!r.Path.All(x => Char.IsUpper(x)) && r.Path.Contains(_pattern))
            {
                r = r.WithAll(r.Path.ToUpperInvariant(), r.Url, r.Details);
            }
            return base.Visit(r);
        }

        public override GitRepository Visit(GitRepository r)
        {
            return base.Visit(r);
        }

    }
}

using System;
using System.Collections.Immutable;

namespace GoM.Core.Immutable.Visitors
{
    public class Visitor
    {
        public virtual GoMContext Visit(GoMContext c)
        {
            var repos = Visit(c.Repositories, Visit);
            //var feeds = Visit(c.Feeds, Visit);
            if (repos != c.Repositories
                /*|| feeds != c.Feeds*/)
            {
                return GoMContext.Create(c.RootPath, repos, c.Feeds/*feeds*/);
            }
            return c;
        }

        protected virtual BasicGitRepository Visit(BasicGitRepository r)
        {
            var dV = r.Details != null ? Visit(r.Details) : null;
            return dV != r.Details
                    ? (dV == null ? BasicGitRepository.Create(r.Path, r.Url) : BasicGitRepository.Create(dV))
                    : r;
        }

        protected virtual GitRepository Visit(GitRepository r)
        {
            return r;
        }

        protected virtual PackageFeed Visit(PackageFeed p)
        {
            throw new NotImplementedException();
        }

        static ImmutableList<T> Visit<T>(ImmutableList<T> input, Func<T, T> transformer) where T : class
        {
            int i = 0;
            ImmutableList<T>.Builder listBuilder = null;
            foreach (var r in input)
            {
                T rV = transformer(r);
                if (rV != r && listBuilder == null)
                {
                    listBuilder = ImmutableList.CreateBuilder<T>();
                    listBuilder.AddRange(input.GetRange(0, i));
                }
                if (listBuilder != null && rV != null) listBuilder.Add(rV);
                ++i;
            }
            return listBuilder == null ? input : listBuilder.ToImmutableList();
        }

    }
}

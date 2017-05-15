using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Mutable
{
    public class GoMActions : IGoMActions
    {
        public GomContext Refresh(GomContext c)
        {
            throw new NotImplementedException();
        }

        IGomContext IGoMActions.Refresh(IGomContext c) => Refresh((GomContext)c);
    }
}

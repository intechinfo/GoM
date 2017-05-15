using GoM.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoM.Core.Persistence
{
    public class GoMActions : IGoMActions
    {
        public GoMContext Refresh(GoMContext c)
        {
            throw new NotImplementedException();
        }

        IGoMContext IGoMActions.Refresh(IGoMContext c) => Refresh((GoMContext)c);
    }
}

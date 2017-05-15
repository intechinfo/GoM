using GoM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoM.Core;

namespace GoM.Persistence
{
    class Persistence : IPersistence
    {

        public IGomContext Load ()
        {
            throw new NotImplementedException();
        }

        public void Save ( IGomContext ctx)
        {
            if ( ctx == null ) throw new ArgumentNullException();



            throw new NotImplementedException();
        }


    }
}

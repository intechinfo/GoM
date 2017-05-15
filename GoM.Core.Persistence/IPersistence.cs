using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoM.Core;

namespace GoM.Persistence
{
    interface IPersistence
    {

        IGoMContext Load ();


        void Save (IGoMContext ctx);


    }
}

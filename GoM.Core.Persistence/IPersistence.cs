using GoM.Core; using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoM.Core; using System;

namespace GoM.Core.Persistence
{
    interface IPersistence
    {

        IGoMContext Load ( string rootPath );


        void Save (IGoMContext ctx);


    }
}

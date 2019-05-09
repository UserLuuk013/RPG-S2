using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface IGevechtContext
    {
        bool VoegGevechtToe(Gevecht gevecht);
        bool VerwijderGevecht(Gevecht gevecht);
        bool GevechtBeëindigd(int xp, int geld, int userId);
    }
}

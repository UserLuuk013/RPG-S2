using RPG_IB2;
using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.TestContexts
{
    public class GevechtTestContext : IGevechtContext
    {
        public bool VoegGevechtToe(Gevecht gevecht)
        {
            return false;
        }
        public bool VerwijderGevecht(Gevecht gevecht)
        {
            return false;
        }
        public bool GevechtBeëindigd(int xp, int geld, int userId)
        {
            return false;
        }
    }
}

using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Repositories
{
    class GevechtRepository
    {
        private readonly IGevechtContext context;
        public GevechtRepository(IGevechtContext context)
        {
            this.context = context;
        }
        public bool VoegGevechtToe(Gevecht gevecht)
        {
            return context.VoegGevechtToe(gevecht);
        }
        public bool VerwijderGevecht(Gevecht gevecht)
        {
            return context.VerwijderGevecht(gevecht);
        }
        public bool GevechtBeëindigd(int xp, int geld, int userId)
        {
            return context.GevechtBeëindigd(xp, geld, userId);
        }
    }
}

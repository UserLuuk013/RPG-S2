using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2.Datalayer.Repositories
{
    class SpelerRepository
    {
        private readonly ISpelerContext context;
        public SpelerRepository(ISpelerContext context)
        {
            this.context = context;
        }
        public bool VoegSpelerToe(Speler speler)
        {
            return context.VoegSpelerToe(speler);
        }
        public bool VerwijderSpeler(Speler speler)
        {
            return context.VerwijderSpeler(speler);
        }
        public Speler GetSpeler(int spelerId)
        {
            return context.GetSpeler(spelerId);
        }
        public bool NieuwSpel(int spelerId)
        {
            return context.NieuwSpel(spelerId);
        }
    }
}

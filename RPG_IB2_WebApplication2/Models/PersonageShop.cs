using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class PersonageShop
    {
        public int XP { get; set; }
        public Personage SpelerPersonage { get; set; }
        public List<Personage> ShopPersonages { get; set; }
    }
}

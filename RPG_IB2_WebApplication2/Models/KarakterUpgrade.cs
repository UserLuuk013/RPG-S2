using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class KarakterUpgrade
    {
        public int XP { get; set; }
        public Karakter SpelerKarakter { get; set; }
        public List<Karakter> ShopKarakters { get; set; }
    }
}

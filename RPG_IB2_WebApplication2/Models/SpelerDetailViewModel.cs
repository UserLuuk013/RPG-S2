using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class SpelerDetailViewModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public int Geld { get; set; }
        public ItemDetailViewModel Wapen { get; set; }
        public ItemDetailViewModel Potion { get; set; }
        public PersonageDetailViewModel Personage { get; set; }
    }
}

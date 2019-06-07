using RPG_IB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class GevechtDetailViewModel
    {
        public Speler Speler { get; set; }
        public Cpu CPU { get; set; }
        public bool SpelerAanZet { get; set; }
        public bool PotionSpelerGebruikt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2
{
    public class Gevecht
    {
        public Speler Speler { get; set; }
        public CPU CPU { get; set; }
        public int Level { get; set; }
        public string Datum { get; set; }
        public string Tijd { get; set; }
        public string Winnaar { get; set; }
        public bool Voltooid { get; set; }
        public bool SpelerAanZet { get; set; }
        public bool PotionSpelerGebruikt { get; set; }
    }
}

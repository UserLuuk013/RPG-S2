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
        public Cpu CPU { get; set; }
        public bool SpelerAanZet { get; set; }
        public bool PotionSpelerGebruikt { get; set; }
    }
}

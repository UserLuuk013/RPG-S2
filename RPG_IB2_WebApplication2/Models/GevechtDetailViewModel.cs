using RPG_IB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class GevechtDetailViewModel
    {
        public enum Superaanval { Geen, Mislukt, Geslaagd }
        public Speler Speler { get; set; }
        public Cpu CPU { get; set; }
        public bool SpelerAanZet { get; set; }
        public bool PotionSpelerGebruikt { get; set; }
        public bool PotionCPUGebruikt { get; set; }
        public bool SpelerLevend { get; set; }
        public bool CPULevend { get; set; }
        public bool GameGestart { get; set; }
        public string Beloningen { get; set; }
        public Superaanval SuperAanval { get; set; }
    }
}

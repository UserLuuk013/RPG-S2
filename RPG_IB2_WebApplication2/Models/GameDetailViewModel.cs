using RPG_IB2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class GameDetailViewModel
    {
        public Speler Speler { get; set; }
        public List<Cpu> CPUs { get; set; }
    }
}

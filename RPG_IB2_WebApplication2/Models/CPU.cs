using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2
{
    public class Cpu
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int HP { get; set; }
        public string Foto { get; set; }
        public Item Wapen { get; set; }
        public Item Potion { get; set; }
    }
}

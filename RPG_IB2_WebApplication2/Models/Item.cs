using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Models
{
    public class Item
    {
        public string Naam { get; set; }
        public int HP { get; set; }
        public int Prijs { get; set; }
        public int ID { get; set; }
        public string Type { get; set; }
        public override string ToString()
        {
            return $"{ Naam}";
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Models
{
    public class Karakter
    {
        public string Naam { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        public int IDKarakter { get; set; }
        public int Prijs { get; set; }
        public override string ToString()
        {
            return Naam;
        }
    }
}

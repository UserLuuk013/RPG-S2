﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2
{
    public class Speler
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int HP { get; set; }
        public int XP { get; set; }
        public int Geld { get; set; }
        public Item Wapen { get; set; }
        public Item Potion { get; set; }
        public Personage Personage { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class PersonageDetailViewModel
    {
        public int ID { get; set; }
        public string Naam { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        public int Prijs { get; set; }
        public string Foto { get; set; }
        public string AlternateText { get; set; }
    }
}

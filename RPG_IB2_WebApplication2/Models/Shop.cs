﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Models
{
    public class Shop
    {
        public int Geld { get; set; }
        public List<Item> PlayerItems { get; set; }
        public List<Item> ShopItems { get; set; }
    }
}

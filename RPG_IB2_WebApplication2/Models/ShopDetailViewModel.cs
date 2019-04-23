using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class ShopDetailViewModel
    {
        public int Geld { get; set; }
        public List<Item> PlayerItems { get; set; }
        public List<Item> ShopItems { get; set; }
    }
}

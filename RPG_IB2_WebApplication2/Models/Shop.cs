using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Models
{
    class Shop
    {
        private int geld;
        private List<Item> items = new List<Item>();
        public Shop(int geld, List<Item> items)
        {
            this.geld = geld;
            this.items = items;
        }
        public int Geld
        {
            get
            {
                return this.geld;
            }
            set
            {
                this.geld = value;
            }
        }
        public List<Item> Items
        {
            get
            {
                return this.items;
            }
            set
            {
                this.items = value;
            }
        }
    }
}

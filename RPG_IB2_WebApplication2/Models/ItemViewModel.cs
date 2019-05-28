 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class ItemViewModel
    {
        private List<ItemDetailViewModel> items = new List<ItemDetailViewModel>();
        public List<ItemDetailViewModel> Items
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

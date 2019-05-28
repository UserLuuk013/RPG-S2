using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class ShopViewModel
    {
        private List<ShopDetailViewModel> shops = new List<ShopDetailViewModel>();
        public List<ShopDetailViewModel> Shops
        {
            get
            {
                return this.shops;
            }
            set
            {
                this.shops = value;
            }
        }
    }
}

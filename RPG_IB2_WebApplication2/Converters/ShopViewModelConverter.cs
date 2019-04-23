using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class ShopViewModelConverter
    {
        public Shop ViewModelToShop(ShopDetailViewModel vm)
        {
            Shop s = new Shop()
            {
                Geld = vm.Geld,
                PlayerItems = vm.PlayerItems,
                ShopItems = vm.ShopItems
            };
            return s;
        }
        public ShopDetailViewModel ViewModelFromShop(Shop s)
        {
            ShopDetailViewModel vm = new ShopDetailViewModel()
            {
                Geld = s.Geld,
                PlayerItems = s.PlayerItems,
                ShopItems = s.ShopItems
            };
            return vm;
        }
    }
}

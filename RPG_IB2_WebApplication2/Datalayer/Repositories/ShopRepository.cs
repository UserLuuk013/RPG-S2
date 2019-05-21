using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Repositories
{
    class ShopRepository
    {
        private IShopContext context;
        public ShopRepository(IShopContext context)
        {
            this.context = context;
        }
        public bool VoegShopToe(Shop shop)
        {
            return context.VoegShopToe(shop);
        }
        public bool VerwijderShop(Shop shop)
        {
            return context.VerwijderShop(shop);
        }
        public List<Item> GetShopItems(int userId)
        {
            return context.GetShopItems(userId);
        }
        public bool KoopItem(int idItem, string type, int geld, int userId)
        {
            return context.KoopItem(idItem, type, geld, userId);
        }
        public bool VerkoopItem(int idItem, string type, int geld, int userId)
        {
            return context.VerkoopItem(idItem, type, geld, userId);
        }
    }
}

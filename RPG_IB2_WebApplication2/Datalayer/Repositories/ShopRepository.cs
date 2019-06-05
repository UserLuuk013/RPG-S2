using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Repositories
{
    public class ShopRepository
    {
        private readonly IShopContext context;
        public ShopRepository(IShopContext context)
        {
            this.context = context;
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

using RPG_IB2.Datalayer.MSSQLContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Controller
{
    class ShopController
    {
        ShopRepository repo = new ShopRepository(new ShopMSSQLContext());
        public bool VoegShopToe(Shop shop)
        {
            return repo.VoegShopToe(shop);
        }
        public bool VerwijderShop(Shop shop)
        {
            return repo.VerwijderShop(shop);
        }
        public List<Item> GetShopItems()
        {
            return repo.GetShopItems();
        }
        public bool KoopItem(int idItem, string type, int geld)
        {
            return repo.KoopItem(idItem, type, geld);
        }
        public bool VerkoopItem(int idItem, string type, int geld)
        {
            return repo.VerkoopItem(idItem, type, geld);
        }
    }
}

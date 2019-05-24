using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.TestContexts
{
    public class ShopTestContext : IShopContext
    {
        public bool VoegShopToe(Shop shop)
        {
            return false;
        }
        public bool VerwijderShop(Shop shop)
        {
            return false;
        }
        public List<Item> GetShopItems(int userId)
        {
            List<Item> items = new List<Item>();
            return items;
        }
        public bool KoopItem(int idItem, string type, int geld, int userId)
        {
            Item item = new Item();
            item.Prijs = 500;
            if (idItem != 0)
            {
                if (geld >= item.Prijs)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool VerkoopItem(int idItem, string type, int geld, int userId)
        {
            if (idItem != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    public interface IShopContext
    {
        List<Item> GetShopItems(int userId);
        bool KoopItem(int idItem, string type, int geld, int userId);
        bool VerkoopItem(int idItem, string type, int geld, int userId);
    }
}

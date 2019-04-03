using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface IItemContext
    {
        int VoegItemToe(Item item);
        bool UpdateItem(Item item);
        bool VerwijderItem(Item item);
        bool UpgradeWapens(int wapenDamage);
        Item GetItemById(int id);
        List<Item> GetAllItems();
    }
}

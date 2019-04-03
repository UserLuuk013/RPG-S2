using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2.Datalayer.Repositories
{
    class ItemRepository
    {
        private IItemContext context;
        public ItemRepository(IItemContext context)
        {
            this.context = context;
        }
        public int VoegItemToe(Item item)
        {
            return context.VoegItemToe(item);
        }
        public bool VerwijderItem(Item item)
        {
            return context.VerwijderItem(item);
        }
        public bool UpgradeWapens(int wapenDamage)
        {
            return context.UpgradeWapens(wapenDamage);
        }
    }
}

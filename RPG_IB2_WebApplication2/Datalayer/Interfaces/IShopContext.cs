﻿using RPG_IB2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface IShopContext
    {
        bool VoegShopToe(Shop shop);
        bool VerwijderShop(Shop shop);
        List<Item> GetShopItems();
        bool KoopItem(int idItem, string type, int geld);
        bool VerkoopItem(int idItem, string type, int geld);
    }
}

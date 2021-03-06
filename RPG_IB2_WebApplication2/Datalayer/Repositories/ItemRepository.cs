﻿using RPG_IB2.Datalayer.Interfaces;
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
        private readonly IItemContext context;
        public ItemRepository(IItemContext context)
        {
            this.context = context;
        }
        public int VoegItemToe(Item item)
        {
            return context.VoegItemToe(item);
        }
        public bool UpdateItem(Item item)
        {
            return context.UpdateItem(item);
        }
        public bool VerwijderItem(Item item)
        {
            return context.VerwijderItem(item);
        }
        public Item GetItemById(int id)
        {
            return context.GetItemById(id);
        }
        public List<Item> GetAllItems()
        {
            return context.GetAllItems();
        }
        public List<Item> GetPlayerItemsById(int spelerId)
        {
            return context.GetPlayerItemsById(spelerId);
        }
        public List<Item> GetCPUItemsById(int cpuId)
        {
            return context.GetCPUItemsById(cpuId);
        }
    }
}

using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class ItemViewModelConverter
    {
        public Item ViewModelToItem(ItemDetailViewModel vm)
        {
            Item i = new Item()
            {
                ID = vm.Id,
                Naam = vm.Naam,
                HP = vm.HP,
                Prijs = vm.Prijs,
                Type = vm.Type
            };
            return i;
        }
        public ItemDetailViewModel ViewModelFromItem(Item i)
        {
            ItemDetailViewModel vm = new ItemDetailViewModel()
            {
                Id = i.ID,
                Naam = i.Naam,
                HP = i.HP,
                Prijs = i.Prijs,
                Type = i.Type
            };
            return vm;
        }
    }
}

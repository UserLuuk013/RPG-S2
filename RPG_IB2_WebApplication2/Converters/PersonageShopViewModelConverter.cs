﻿using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class PersonageShopViewModelConverter
    {
        public PersonageShop ViewModelToPersonageShop(PersonageShopDetailViewModel vm)
        {
            PersonageShop p = new PersonageShop()
            {
                XP = vm.XP,
                SpelerPersonage = vm.SpelerPersonage,
                VolgendeUpgrade = vm.VolgendeUpgrade
            };
            return p;
        }
        public PersonageShopDetailViewModel ViewModelFromPersonageShop(PersonageShop p)
        {
            PersonageShopDetailViewModel vm = new PersonageShopDetailViewModel()
            {
                XP = p.XP,
                SpelerPersonage = p.SpelerPersonage,
                VolgendeUpgrade = p.VolgendeUpgrade
            };
            return vm;
        }
    }
}

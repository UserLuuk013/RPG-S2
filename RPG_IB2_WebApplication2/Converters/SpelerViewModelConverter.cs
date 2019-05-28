using RPG_IB2;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class SpelerViewModelConverter
    {
        private readonly ItemViewModelConverter itemcvt = new ItemViewModelConverter();
        private readonly PersonageViewModelConverter personagecvt = new PersonageViewModelConverter();
        public Speler ViewModelToSpeler(SpelerDetailViewModel vm)
        {
            Speler s = new Speler()
            {
                ID = vm.ID,
                Naam = vm.Naam,
                HP = vm.HP,
                XP = vm.XP,
                Geld = vm.Geld,
                Wapen = itemcvt.ViewModelToItem(vm.Wapen),
                Potion = itemcvt.ViewModelToItem(vm.Potion),
                Personage = personagecvt.ViewModelToPersonage(vm.Personage)
            };
            return s;
        }
        public SpelerDetailViewModel ViewModelFromSpeler(Speler s)
        {
            SpelerDetailViewModel vm = new SpelerDetailViewModel()
            {
                ID = s.ID,
                Naam = s.Naam,
                HP = s.HP,
                XP = s.HP,
                Geld = s.Geld,
                Wapen = itemcvt.ViewModelFromItem(s.Wapen),
                Potion = itemcvt.ViewModelFromItem(s.Potion),
                Personage = personagecvt.ViewModelFromPersonage(s.Personage)
            };
            return vm;
        }
    }
}

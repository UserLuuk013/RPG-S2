using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class KarakterViewModelConverter
    {
        public Karakter ViewModelToKarakter(KarakterDetailViewModel vm)
        {
            Karakter k = new Karakter()
            {
                IDKarakter = vm.IDKarakter,
                Naam = vm.Naam,
                HP = vm.HP,
                Damage = vm.Damage,
                Prijs = vm.Prijs
            };
            return k;
        }
        public KarakterDetailViewModel ViewModelFromKarakter(Karakter k)
        {
            KarakterDetailViewModel vm = new KarakterDetailViewModel()
            {
                IDKarakter = k.IDKarakter,
                Naam = k.Naam,
                HP = k.HP,
                Damage = k.Damage,
                Prijs = k.Prijs
            };
            return vm;
        }
    }
}

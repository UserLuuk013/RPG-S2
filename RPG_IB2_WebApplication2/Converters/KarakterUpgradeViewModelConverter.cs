using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class KarakterUpgradeViewModelConverter
    {
        public KarakterUpgrade ViewModelToKarakterUpgrade(KarakterUpgradeDetailViewModel vm)
        {
            KarakterUpgrade k = new KarakterUpgrade()
            {
                XP = vm.XP,
                SpelerKarakter = vm.SpelerKarakter,
                ShopKarakters = vm.ShopKarakters
            };
            return k;
        }
        public KarakterUpgradeDetailViewModel ViewModelFromKarakterUpgrade(KarakterUpgrade k)
        {
            KarakterUpgradeDetailViewModel vm = new KarakterUpgradeDetailViewModel()
            {
                XP = k.XP,
                SpelerKarakter = k.SpelerKarakter,
                ShopKarakters = k.ShopKarakters
            };
            return vm;
        }
    }
}

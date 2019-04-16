using RPG_IB2;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class GevechtViewModelConverter
    {
        public Gevecht ViewModelToGevecht(GevechtDetailViewModel vm)
        {
            Gevecht g = new Gevecht()
            {
                Speler = vm.Speler,
                CPU = vm.CPU
            };
            return g;
        }
        public GevechtDetailViewModel ViewModelFromGevecht(Gevecht g)
        {
            GevechtDetailViewModel vm = new GevechtDetailViewModel()
            {
                Speler = g.Speler,
                CPU = g.CPU
            };
            return vm;
        }
    }
}

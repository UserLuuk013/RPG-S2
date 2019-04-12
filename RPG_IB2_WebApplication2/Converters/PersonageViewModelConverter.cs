using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Converters
{
    public class PersonageViewModelConverter
    {
        public Personage ViewModelToPersonage(PersonageDetailViewModel vm)
        {
            Personage p = new Personage()
            {
                ID = vm.ID,
                Naam = vm.Naam,
                Foto = vm.Foto,
                AlternateText = vm.AlternateText
            };
            return p;
        }
        public PersonageDetailViewModel ViewModelFromPersonage(Personage p)
        {
            PersonageDetailViewModel vm = new PersonageDetailViewModel()
            {
                ID = p.ID,
                Naam = p.Naam,
                Foto = p.Foto,
                AlternateText = p.AlternateText
            };
            return vm;
        }
    }
}

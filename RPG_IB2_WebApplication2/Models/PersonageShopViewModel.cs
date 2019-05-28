using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class PersonageShopViewModel
    {
        private List<PersonageShopDetailViewModel> personageshops = new List<PersonageShopDetailViewModel>();
        public List<PersonageShopDetailViewModel> PersonageShops
        {
            get
            {
                return this.personageshops;
            }
            set
            {
                this.personageshops = value;
            }
        }
    }
}

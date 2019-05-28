using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class PersonageViewModel
    {
        private List<PersonageDetailViewModel> personages = new List<PersonageDetailViewModel>();
        public List<PersonageDetailViewModel> Personages
        {
            get
            {
                return this.personages;
            }
            set
            {
                this.personages = value;
            }
        }
    }
}

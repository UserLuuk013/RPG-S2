using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class SpelerViewModel
    {
        private List<SpelerDetailViewModel> spelers = new List<SpelerDetailViewModel>();
        public List<SpelerDetailViewModel> Spelers
        {
            get
            {
                return this.spelers;
            }
            set
            {
                this.spelers = value;
            }
        }
    }
}

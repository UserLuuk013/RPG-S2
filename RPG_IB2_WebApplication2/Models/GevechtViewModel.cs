using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Models
{
    public class GevechtViewModel
    {
        private List<GevechtDetailViewModel> gevechten = new List<GevechtDetailViewModel>();
        public List<GevechtDetailViewModel> Gevechten
        {
            get
            {
                return this.gevechten;
            }
            set
            {
                this.gevechten = value;
            }
        }
    }
}

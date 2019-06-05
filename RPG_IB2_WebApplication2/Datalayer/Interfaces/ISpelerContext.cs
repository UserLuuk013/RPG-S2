using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface ISpelerContext
    {
        Speler GetSpelerByID(int spelerId);
        bool NieuwSpel(int spelerId);
    }
}

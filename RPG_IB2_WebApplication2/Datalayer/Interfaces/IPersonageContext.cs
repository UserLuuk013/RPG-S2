using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.Interfaces
{
    interface IPersonageContext
    {
        int VoegPersonageToe(Personage personage);
        bool UpdatePersonage(Personage personage);
        bool VerwijderPersonage(Personage personage);
        List<Personage> GetAllPersonages();
        Personage GetPersonageById(int id);
        bool SelecteerPersonage(int id);
    }
}

using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.Interfaces
{
    public interface IPersonageContext
    {
        List<Personage> GetAllPersonages();
        Personage GetPersonageById(int id);
        bool SelecteerPersonage(int id, int userId);
        Personage GetPersonageBySpelerId(int spelerId);
        List<Personage> GetPersonagesBySpelerId(int spelerId);
        List<Personage> GetStartPersonages();
        bool UpgradePersonage(int personageId, int spelerXP, int userId);
    }
}

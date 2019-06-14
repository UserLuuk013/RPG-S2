using RPG_IB2_WebApplication2.Datalayer.Interfaces;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.Repositories
{
    public class PersonageRepository
    {
        private readonly IPersonageContext context;
        public PersonageRepository(IPersonageContext context)
        {
            this.context = context;
        }
        public List<Personage> GetAllPersonages()
        {
            return context.GetAllPersonages();
        }
        public Personage GetPersonageById(int id)
        {
            return context.GetPersonageById(id);
        }
        public bool SelecteerPersonage(int id, int userId)
        {
            return context.SelecteerPersonage(id, userId);
        }
        public Personage GetPersonageBySpelerId(int spelerId)
        {
            return context.GetPersonageBySpelerId(spelerId);
        }
        public Personage GetNextPersonageUpgradeBySpelerId(int spelerId)
        {
            return context.GetNextUpgradePersonageBySpelerId(spelerId);
        }
        public List<Personage> GetStartPersonages()
        {
            return context.GetStartPersonages();
        }
        public bool UpgradePersonage(int personageId, int spelerXP, int userId, int personageHP)
        {
            return context.UpgradePersonage(personageId, spelerXP, userId, personageHP);
        }
    }
}

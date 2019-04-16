using RPG_IB2_WebApplication2.Datalayer.Interfaces;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.Repositories
{
    class PersonageRepository
    {
        private IPersonageContext context;
        public PersonageRepository(IPersonageContext context)
        {
            this.context = context;
        }
        public int VoegPersonageToe(Personage personage)
        {
            return context.VoegPersonageToe(personage);
        }
        public bool UpdatePersonage(Personage personage)
        {
            return context.UpdatePersonage(personage);
        }
        public bool VerwijderPersonage(Personage personage)
        {
            return context.VerwijderPersonage(personage);
        }
        public List<Personage> GetAllPersonages()
        {
            return context.GetAllPersonages();
        }
        public Personage GetPersonageById(int id)
        {
            return context.GetPersonageById(id);
        }
        public bool SelecteerPersonage(int id)
        {
            return context.SelecteerPersonage(id);
        }
        public Personage GetPersonageBySpelerId(int spelerId)
        {
            return context.GetPersonageBySpelerId(spelerId);
        }
    }
}

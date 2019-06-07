using RPG_IB2_WebApplication2.Datalayer.Interfaces;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.TestContexts
{
    public class PersonageTestContext : IPersonageContext
    {
        public int VoegPersonageToe(Personage personage)
        {
            return 0;
        }
        public bool UpdatePersonage(Personage personage)
        {
            return false;
        }
        public bool VerwijderPersonage(Personage personage)
        {
            return false;
        }
        public List<Personage> GetAllPersonages()
        {
            List<Personage> personages = new List<Personage>();
            return personages;
        }
        public Personage GetPersonageById(int id)
        {
            Personage personage = new Personage();
            return personage;
        }
        public bool SelecteerPersonage(int id, int userId)
        {
            if (id != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Personage GetPersonageBySpelerId(int spelerId)
        {
            Personage personage = new Personage();
            return personage;
        }
        public List<Personage> GetPersonagesBySpelerId(int spelerId)
        {
            List<Personage> personages = new List<Personage>();
            return personages;
        }
        public List<Personage> GetStartPersonages()
        {
            List<Personage> personages = new List<Personage>();
            return personages;
        }
        public bool UpgradePersonage(int personageId, int spelerXP, int userId)
        {
            Personage personage = new Personage();
            personage.Prijs = 1000;
            if (personageId != 0 && userId != 0)
            {
                if (personage.Prijs <= spelerXP)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
    }
}

using RPG_IB2.Datalayer;
using RPG_IB2_WebApplication2.Datalayer.Interfaces;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RPG_IB2_WebApplication2.Datalayer.MSSQLContexts
{
    class PersonageMSSQLContext : DataConnection, IPersonageContext
    {
        private EquipDomein equipDomein;
        public PersonageMSSQLContext()
        {
            equipDomein = new EquipDomein();
        }
        public List<Personage> GetAllPersonages()
        {
            List<Personage> personages = new List<Personage>();

            SqlCommand myCommand = SetCommandProcedure("GetAllPersonages");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    personages = equipDomein.VulPersonages(myReader, personages);
                }
            }
            return personages;
        }
        public Personage GetPersonageById(int id)
        {
            SqlCommand myCommand = SetCommandProcedure("GetPersonageById");
            myCommand.Parameters.AddWithValue("@IDPersonage", id);
            Personage personage = new Personage();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    personage = equipDomein.VulPersonage(myReader, personage);
                }
            }
            return personage;
        }
        public bool SelecteerPersonage(int id, int userId)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("SelecteerPersonage");
                myCommand.Parameters.AddWithValue("@IDPersonage", id);
                myCommand.Parameters.AddWithValue("@IDAccount", userId);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                return false;
            }
        }
        public Personage GetPersonageBySpelerId(int spelerId)
        {
            SqlCommand myCommand = SetCommandProcedure("GetPersonageBySpelerId");
            myCommand.Parameters.AddWithValue("@IDAccount", spelerId);
            Personage personage = new Personage();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    personage = equipDomein.VulPersonage(myReader, personage);
                }
            }
            return personage;
        }
        public List<Personage> GetPersonagesBySpelerId(int spelerId)
        {
            List<Personage> personages = new List<Personage>();

            SqlCommand myCommand = SetCommandProcedure("GetPersonagesBySpelerId");
            myCommand.Parameters.AddWithValue("@IDAccount", spelerId);
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    personages = equipDomein.VulPersonages(myReader, personages);
                }
            }
            return personages;
        }
        public List<Personage> GetAllStartPersonages()
        {
            List<Personage> personages = new List<Personage>();

            SqlCommand myCommand = SetCommandProcedure("GetAllStartPersonages");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    personages = equipDomein.VulPersonages(myReader, personages);
                }
            }
            return personages;
        }
        public bool UpgradePersonage(int personageId, int spelerXP, int userId)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("UpgradePersonage");
                myCommand.Parameters.AddWithValue("@IDPersonage", personageId);
                myCommand.Parameters.AddWithValue("@XP", spelerXP);
                myCommand.Parameters.AddWithValue("@IDAccount", userId);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                return false;
            }
        }
    }
}

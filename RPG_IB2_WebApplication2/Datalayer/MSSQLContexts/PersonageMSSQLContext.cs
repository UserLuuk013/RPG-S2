﻿using RPG_IB2.Datalayer;
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
        public PersonageMSSQLContext()
        {
            //
        }
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

            SqlCommand myCommand = SetCommandProcedure("GetAllPersonages");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    Personage personage = new Personage();

                    personage.ID = Convert.ToInt32(myReader["ID-Personage"]);
                    personage.Naam = Convert.ToString(myReader["Naam"]);
                    personage.Foto = Convert.ToString(myReader["Foto"]);
                    personage.AlternateText = Convert.ToString(myReader["AlternateText"]);

                    personages.Add(personage);
                }
            }
            return personages;
        }
        public Personage GetPersonageById(int id)
        {
            Personage personage = new Personage();
            return personage;
        }
        public bool SelecteerPersonage(int id)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("SelecteerPersonage");
                myCommand.Parameters.AddWithValue("@IDPersonage", id);
                myCommand.Parameters.AddWithValue("@IDAccount", 1);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }
    }
}
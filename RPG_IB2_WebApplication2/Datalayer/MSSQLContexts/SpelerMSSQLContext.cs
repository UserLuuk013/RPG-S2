using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class SpelerMSSQLContext : DataConnection, ISpelerContext
    {
        public SpelerMSSQLContext()
        {
            //
        }
        public bool VoegSpelerToe(Speler speler)
        {
            return false;
        }
        public bool VerwijderSpeler(Speler speler)
        {
            return false;
        }
        public Speler GetSpeler(int spelerId)
        {
            SqlCommand myCommand = SetCommandProcedure("GetSpeler");
            myCommand.Parameters.AddWithValue("@IDAccount", spelerId);
            Speler speler = new Speler();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    speler.ID = Convert.ToInt32(myReader["ID-Account"]);
                    speler.Naam = Convert.ToString(myReader["Naam"]);
                    speler.HP = Convert.ToInt32(myReader["HP"]);
                    speler.XP = Convert.ToInt32(myReader["XP"]);
                    speler.Geld = Convert.ToInt32(myReader["Geld"]);
                }
            }
            return speler;
        }
    }
}

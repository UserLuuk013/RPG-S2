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
        public List<Item> GetPlayerItems()
        {
            List<Item> items = new List<Item>();

            SqlCommand myCommand = SetCommandProcedure("GetPlayerItems");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    Item item = new Item();

                    item.Naam = Convert.ToString(myReader["Naam"]);
                    item.Prijs = Convert.ToInt32(myReader["Prijs"]);
                    item.HP = Convert.ToInt32(myReader["HP"]);
                    item.ID = Convert.ToInt32(myReader["ID-Item"]);
                    item.Type = Convert.ToString(myReader["Type"]);

                    items.Add(item);
                }
            }
            return items;
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

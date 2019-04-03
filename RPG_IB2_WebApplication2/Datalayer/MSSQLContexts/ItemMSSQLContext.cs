using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;
using System.Data.SqlClient;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class ItemMSSQLContext : DataConnection, IItemContext
    {
        public ItemMSSQLContext()
        {
            //
        }
        public int VoegItemToe(Item item)
        {
            int Id = 0;
            SqlCommand myCommand = SetCommandProcedure("VoegItemToe");
            myCommand.Parameters.AddWithValue("@Naam", item.Naam);
            myCommand.Parameters.AddWithValue("@HP", 10);
            myCommand.Parameters.AddWithValue("@Prijs", 2000);
            myCommand.Parameters.AddWithValue("@Type", "Wapen");
            //myCommand.ExecuteNonQuery();

            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    Id = Convert.ToInt32(myReader["IDItem"]);
                }
            }
            return Id;
        }
        public bool VerwijderItem(Item item)
        {
            return false;
        }
        public bool UpgradeWapens(int wapenDamage)
        {
            try
            {
                for (int i = 1; i < 7; i++)
                {
                    SqlCommand myCommand = SetCommandProcedure("UpgradeWapens");
                    myCommand.Parameters.AddWithValue("@WapenDamage", wapenDamage);
                    myCommand.Parameters.AddWithValue("IDItem", i);
                    myCommand.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
            
        }
    }
}

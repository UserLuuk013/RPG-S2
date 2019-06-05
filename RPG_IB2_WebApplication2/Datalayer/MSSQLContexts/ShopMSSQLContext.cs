using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class ShopMSSQLContext : DataConnection, IShopContext
    {
        private EquipDomein equipDomein;
        public ShopMSSQLContext()
        {
            equipDomein = new EquipDomein();
        }
        public List<Item> GetShopItems(int userId)
        {
            List<Item> items = new List<Item>();

            SqlCommand myCommand = SetCommandProcedure("GetShopItems");
            myCommand.Parameters.AddWithValue("@IDAccount", userId);
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    items = equipDomein.VulItems(myReader, items);
                }
            }
            return items;
        }
        public bool KoopItem(int idItem, string type, int geld, int userId)
        {
            if (type == "Wapen ")
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("KoopWapen");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
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
            else
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("KoopPotion");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
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
        public bool VerkoopItem(int idItem, string type, int geld, int userId)
        {
            if (type == "Wapen ")
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("VerkoopWapen");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
                    myCommand.Parameters.AddWithValue("@IDAccount", userId);
                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
            else
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("VerkoopPotion");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
                    myCommand.Parameters.AddWithValue("@IDAccount", userId);
                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }
        }
    }
}

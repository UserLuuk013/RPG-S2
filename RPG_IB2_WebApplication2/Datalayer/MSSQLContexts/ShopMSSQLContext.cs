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
    class ShopMssqlContext : DataConnection, IShopContext
    {
        public ShopMssqlContext()
        {
            //
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

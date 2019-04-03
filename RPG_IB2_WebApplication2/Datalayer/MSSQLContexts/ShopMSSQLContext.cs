using RPG_IB2.Datalayer.Interfaces;
using RPG_IB2.Models;
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
        public ShopMSSQLContext()
        {
            //
        }
        public bool VoegShopToe(Shop shop)
        {
            return false;
        }
        public bool VerwijderShop(Shop shop)
        {
            return false;
        }
        public List<Item> GetShopItems()
        {
            List<Item> items = new List<Item>();

            SqlCommand myCommand = SetCommandProcedure("GetShopItems");
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
        public bool KoopItem(int idItem, string type, int geld)
        {
            if (type == "Wapen ")
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("KoopWapen");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception x)
                {
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
                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception x)
                {
                    return false;
                }
            }
        }
        public bool VerkoopItem(int idItem, string type, int geld)
        {
            if (type == "Wapen ")
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("VerkoopWapen");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            else
            {
                try
                {
                    SqlCommand myCommand = SetCommandProcedure("VerkoopWapen");
                    myCommand.Parameters.AddWithValue("@IDItem", idItem);
                    myCommand.Parameters.AddWithValue("@Geld", geld);
                    myCommand.ExecuteNonQuery();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            
        }
    }
}

﻿using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_IB2.Models;
using System.Data.SqlClient;
using RPG_IB2_WebApplication2.Models;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class ItemMssqlContext : DataConnection, IItemContext
    {
        public ItemMssqlContext()
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

            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    Id = Convert.ToInt32(myReader["IDItem"]);
                }
            }
            return Id;
        }
        public bool UpdateItem(Item item)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("UpdateItem");
                myCommand.Parameters.AddWithValue("@IDItem", item.ID);
                myCommand.Parameters.AddWithValue("@Naam", item.Naam);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                return false;
            }
        }
        public bool VerwijderItem(Item item)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("DeleteItem");
                myCommand.Parameters.AddWithValue("@IDItem", item.ID);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                return false;
            }
        }
        public Item GetItemById(int id)
        {
            SqlCommand myCommand = SetCommandProcedure("GetItemById");
            myCommand.Parameters.AddWithValue("@IDItem", id);
            Item item = new Item();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    item.ID = Convert.ToInt32(myReader["ID-Item"]);
                    item.Naam = Convert.ToString(myReader["Naam"]);
                    item.HP = Convert.ToInt32(myReader["HP"]);
                    item.Prijs = Convert.ToInt32(myReader["Prijs"]);
                    item.Type = Convert.ToString(myReader["Type"]);
                }
            }
            return item;
        }
        public List<Item> GetAllItems()
        {
            List<Item> items = new List<Item>();

            SqlCommand myCommand = SetCommandProcedure("GetAllItems");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    items = VulItems(myReader, items);
                }
            }
            return items;
        }
        public List<Item> GetPlayerItemsById(int spelerId)
        {
            List<Item> items = new List<Item>();

            SqlCommand myCommand = SetCommandProcedure("GetPlayerItemsById");
            myCommand.Parameters.AddWithValue("@IDAccount", spelerId);
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    items = VulItems(myReader, items);
                }
            }
            return items;
        }
        public List<Item> GetCPUItemsById(int cpuId)
        {
            List<Item> items = new List<Item>();

            SqlCommand myCommand = SetCommandProcedure("GetCPUItemsById");
            myCommand.Parameters.AddWithValue("@IDCPU", cpuId);
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    items = VulItems(myReader, items);
                }
            }
            return items;
        }
        public List<Item> VulItems(SqlDataReader myReader, List<Item> items)
        {
            Item item = new Item();

            item.Naam = Convert.ToString(myReader["Naam"]);
            item.Prijs = Convert.ToInt32(myReader["Prijs"]);
            item.HP = Convert.ToInt32(myReader["HP"]);
            item.ID = Convert.ToInt32(myReader["ID-Item"]);
            item.Type = Convert.ToString(myReader["Type"]);

            items.Add(item);
            return items;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer
{
    class DataConnection
    {
        private SqlConnection conn;

        public DataConnection() //Nieuwe connectie wordt aangemaakt a.d.h.v. connectionstring.
        {
            try
            {
                conn = new SqlConnection("Server = mssql.fhict.local; Database = dbi409381; User Id = dbi409381; Password = UserLuuk013");
            }
            catch (StackOverflowException ov)
            {
                Console.WriteLine("Error" + ov.Message);
            }
        }

        public SqlConnection OpenConn() //Connectie wordt geopend op het moment dat deze nog gesloten is.
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    Console.WriteLine("Database connection open");
                }
                else
                {
                    Console.WriteLine("Database connection already open");
                }
            }
            catch (Exception x)
            {
                Console.WriteLine("Couldn't connect to datbase: " + x);
                throw;
            }
            return conn;
        }

        public void CloseConn() //Connectie wordt gesloten op het moment dat deze nog geopend is.
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                Console.WriteLine("Closed connection to database");
            }
            else
            {
                Console.WriteLine("Connection already closed");
            }
        }

        public SqlCommand SetCommandProcedure(string procedure) //De stored procedure krijgt vorm en wordt meegegeven.
        {
            try
            {
                conn = OpenConn();
                SqlCommand myCommand = new SqlCommand(procedure, conn);
                myCommand.CommandType = CommandType.StoredProcedure;
                return myCommand;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                CloseConn();
                throw;
            }
        }

        public SqlCommand SetCommandQuery(string query) //De query krijgt vorm en wordt meegegeven.
        {
            try
            {
                conn = OpenConn();
                SqlCommand myCommand = new SqlCommand(query, conn);
                myCommand.CommandType = CommandType.Text;
                return myCommand;
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                CloseConn();
                throw;
            }
        }

        public SqlDataReader ExecuteReader(SqlCommand myCommand) //De Reader wordt geëxecute.
        {
            try
            {
                return myCommand.ExecuteReader();
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
                throw;
            }
        }
    }
}

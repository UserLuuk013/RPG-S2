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
    class KarakterMSSQLContext : DataConnection, IKarakterContext
    {
        public KarakterMSSQLContext()
        {
            //
        }
        public bool VoegKarakterToe(Karakter karakter)
        {
            return false;
        }
        public bool VerwijderKarakter(Karakter karakter)
        {
            return false;
        }
        public Karakter GetSpelerKarakter(int idSpeler)
        {
            SqlCommand myCommand = SetCommandProcedure("GetSpelerKarakter");
            myCommand.Parameters.AddWithValue("@IDAccount", idSpeler);
            Karakter karakter = new Karakter();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    karakter.IDKarakter = Convert.ToInt32(myReader["ID-Karakter"]);
                    karakter.Naam = Convert.ToString(myReader["Naam"]);
                    karakter.HP = Convert.ToInt32(myReader["HP"]);
                    karakter.Damage = Convert.ToInt32(myReader["Damage"]);
                    karakter.Prijs = Convert.ToInt32(myReader["Prijs"]);
                }
            }
            return karakter;
        }
        public List<Karakter> GetAllKarakters(int idSpeler)
        {
            List<Karakter> karakters = new List<Karakter>();

            SqlCommand myCommand = SetCommandProcedure("GetAllKarakters");
            myCommand.Parameters.AddWithValue("@IDAccount", idSpeler);
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    Karakter karakter = new Karakter();

                    karakter.IDKarakter = Convert.ToInt32(myReader["ID-Karakter"]);
                    karakter.Naam = Convert.ToString(myReader["Naam"]);
                    karakter.HP = Convert.ToInt32(myReader["HP"]);
                    karakter.Damage = Convert.ToInt32(myReader["Damage"]);
                    karakter.Prijs = Convert.ToInt32(myReader["Prijs"]);

                    karakters.Add(karakter);
                }
            }
            return karakters;
        }
        public bool UpgradeKarakter(int idKarakter, int xp, int hp)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("UpgradeKarakter");
                myCommand.Parameters.AddWithValue("@IDKarakter", idKarakter);
                myCommand.Parameters.AddWithValue("@XP", xp);
                myCommand.Parameters.AddWithValue("@HP", hp);
                myCommand.ExecuteNonQuery();
                return true;
            }
            catch (Exception x)
            {
                return false;
            }
        }
        public Karakter GetKarakterById(int idKarakter)
        {
            SqlCommand myCommand = SetCommandProcedure("GetKarakterById");
            myCommand.Parameters.AddWithValue("@IDKarakter", idKarakter);
            Karakter karakter = new Karakter();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    karakter.IDKarakter = Convert.ToInt32(myReader["ID-Karakter"]);
                    karakter.Naam = Convert.ToString(myReader["Naam"]);
                    karakter.HP = Convert.ToInt32(myReader["HP"]);
                    karakter.Damage = Convert.ToInt32(myReader["Damage"]);
                    karakter.Prijs = Convert.ToInt32(myReader["Prijs"]);
                }
            }
            return karakter;
        }
    }
}

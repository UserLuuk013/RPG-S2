using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class GevechtMSSQLContext : DataConnection, IGevechtContext
    {
        public GevechtMSSQLContext()
        {
            //
        }
        public bool VoegGevechtToe(Gevecht gevecht)
        {
            return false;
        }
        public bool VerwijderGevecht(Gevecht gevecht)
        {
            return false;
        }
        public bool GevechtBeëindigd(int xp, int geld, int userId)
        {
            try
            {
                SqlCommand myCommand = SetCommandProcedure("GevechtBeëindigd");
                myCommand.Parameters.AddWithValue("@XP", xp);
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
}

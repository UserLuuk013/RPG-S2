using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class CPUMSSQLContext : DataConnection, ICPUContext
    {
        public CPUMSSQLContext()
        {
            //
        }
        public CPU GetCPUById(int id)
        {
            SqlCommand myCommand = SetCommandProcedure("GetCPUById");
            myCommand.Parameters.AddWithValue("@IDCPU", id);
            CPU cpu = new CPU();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    cpu.ID = Convert.ToInt32(myReader["ID-CPU"]);
                    cpu.Naam = Convert.ToString(myReader["Naam"]);
                    cpu.HP = Convert.ToInt32(myReader["HP"]);
                    cpu.Foto = Convert.ToString(myReader["Foto"]);
                }
            }
            return cpu;
        }
        public List<CPU> GetAllCPUs()
        {
            List<CPU> cpus = new List<CPU>();

            SqlCommand myCommand = SetCommandProcedure("GetAllCPUs");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    CPU cpu = new CPU();

                    cpu.ID = Convert.ToInt32(myReader["ID-CPU"]);
                    cpu.Naam = Convert.ToString(myReader["Naam"]);
                    cpu.HP = Convert.ToInt32(myReader["HP"]);
                    cpu.Foto = Convert.ToString(myReader["Foto"]);

                    cpus.Add(cpu);
                }
            }
            return cpus;
        }
    }
}

using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class CpuMssqlContext : DataConnection, ICpuContext
    {
        public CpuMssqlContext()
        {
            //
        }
        public Cpu GetCPUById(int id)
        {
            SqlCommand myCommand = SetCommandProcedure("GetCPUById");
            myCommand.Parameters.AddWithValue("@IDCPU", id);
            Cpu cpu = new Cpu();
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    cpu.ID = Convert.ToInt32(myReader["ID-CPU"]);
                    cpu.Naam = Convert.ToString(myReader["Naam"]);
                    cpu.HP = Convert.ToInt32(myReader["HP"]);
                    cpu.Foto = Convert.ToString(myReader["Foto"]);
                    cpu.XP = Convert.ToInt32(myReader["XP"]);
                    cpu.Geld = Convert.ToInt32(myReader["Geld"]);
                }
            }
            return cpu;
        }
        public List<Cpu> GetAllCPUs()
        {
            List<Cpu> cpus = new List<Cpu>();

            SqlCommand myCommand = SetCommandProcedure("GetAllCPUs");
            using (SqlDataReader myReader = ExecuteReader(myCommand))
            {
                while (myReader.Read())
                {
                    Cpu cpu = new Cpu();

                    cpu.ID = Convert.ToInt32(myReader["ID-CPU"]);
                    cpu.Naam = Convert.ToString(myReader["Naam"]);
                    cpu.HP = Convert.ToInt32(myReader["HP"]);
                    cpu.Foto = Convert.ToString(myReader["Foto"]);
                    cpu.XP = Convert.ToInt32(myReader["XP"]);
                    cpu.Geld = Convert.ToInt32(myReader["Geld"]);

                    cpus.Add(cpu);
                }
            }
            return cpus;
        }
    }
}

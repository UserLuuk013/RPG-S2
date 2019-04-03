using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.MSSQLContexts
{
    class CPUMSSQLContext : ICPUContext
    {
        public CPUMSSQLContext()
        {
            //
        }
        public bool VoegCPUToe(CPU cpu)
        {
            return false;
        }
        public bool VerwijderCPU(CPU cpu)
        {
            return false;
        }
    }
}

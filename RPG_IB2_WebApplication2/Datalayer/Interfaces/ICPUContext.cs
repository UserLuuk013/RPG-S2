using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface ICPUContext
    {
        bool VoegCPUToe(CPU cpu);
        bool VerwijderCPU(CPU cpu);
        CPU GetCPUById(int id);
        List<CPU> GetAllCPUs();
    }
}

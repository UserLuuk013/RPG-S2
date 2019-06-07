using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface ICpuContext
    {
        Cpu GetCPUById(int id);
        List<Cpu> GetAllCPUs();
    }
}

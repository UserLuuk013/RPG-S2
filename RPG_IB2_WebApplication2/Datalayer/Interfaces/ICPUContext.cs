using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Interfaces
{
    interface ICPUContext
    {
        CPU GetCPUById(int id);
        List<CPU> GetAllCPUs();
    }
}

using RPG_IB2.Datalayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_IB2.Datalayer.Repositories
{
    class CPURepository
    {
        private readonly ICpuContext context;
        public CPURepository(ICpuContext context)
        {
            this.context = context;
        }
        public Cpu GetCPUById(int id)
        {
            return context.GetCPUById(id);
        }
        public List<Cpu> GetAllCPUs()
        {
            return context.GetAllCPUs();
        }
    }
}

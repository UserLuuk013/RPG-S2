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
        private readonly ICPUContext context;
        public CPURepository(ICPUContext context)
        {
            this.context = context;
        }
        public CPU GetCPUById(int id)
        {
            return context.GetCPUById(id);
        }
        public List<CPU> GetAllCPUs()
        {
            return context.GetAllCPUs();
        }
    }
}

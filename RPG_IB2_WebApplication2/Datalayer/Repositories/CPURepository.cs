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
        private ICPUContext context;
        public CPURepository(ICPUContext context)
        {
            this.context = context;
        }
        public bool VoegCPUToe(CPU cpu)
        {
            return context.VoegCPUToe(cpu);
        }
        public bool VerwijderCPU(CPU cpu)
        {
            return context.VerwijderCPU(cpu);
        }
    }
}

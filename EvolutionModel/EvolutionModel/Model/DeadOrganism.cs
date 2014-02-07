using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model
{
    public class DeadOrganism
    {
        public int Energy { get; set; }

        public DeadOrganism(int mass)
        {
            Energy += mass;
        }

        public DeadOrganism(int mass, int energy)
        {
            Energy += mass + energy;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    [Serializable]
    public abstract class EnergyFactory : IEnergyFactory
    {
        public abstract int createEnergy(out int waterCount, int mass);
        public abstract IPhenotype Mutate();
    }
}

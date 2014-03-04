using EvolutionModel.Model.Genotypes;
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
        public abstract IPhenotype Mutate();
        public abstract void createEnergy(Plant owner);
    }
}

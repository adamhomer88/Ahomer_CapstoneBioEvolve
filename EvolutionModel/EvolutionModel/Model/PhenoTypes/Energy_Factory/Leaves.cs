using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    [Serializable]
    public class Leaves : EnergyFactory
    {
        public override IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }

        public override void createEnergy(Plant owner)
        {
            throw new NotImplementedException();
        }
    }
}

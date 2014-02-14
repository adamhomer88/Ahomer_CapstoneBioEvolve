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
        public override int createEnergy(out int newWaterTotal, int mass)
        {
            throw new NotImplementedException();
        }

        public override IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}

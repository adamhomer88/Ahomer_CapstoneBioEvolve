using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    public class Leaves : EnergyFactory
    {
        public override int createEnergy(out int newWaterTotal, int mass)
        {
            throw new NotImplementedException();
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            throw new NotImplementedException();
        }

        public override IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    public abstract class EnergyFactory : IEnergyFactory
    {

        public abstract int createEnergy(out int waterCount, int mass);
        public abstract void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context);
        public abstract IPhenotype Mutate();
    }
}

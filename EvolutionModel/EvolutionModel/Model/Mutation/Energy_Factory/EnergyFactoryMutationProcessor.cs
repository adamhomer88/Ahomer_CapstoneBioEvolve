using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Energy_Factory
{
    [Serializable]
    class EnergyFactoryMutationProcessor : IEnergyFactoryMutationProcessor
    {
        private static EnergyFactoryMutationProcessor processor;

        public static EnergyFactoryMutationProcessor GetInstance()
        {
            if (processor == null)
                processor = new EnergyFactoryMutationProcessor();
            return processor;
        }

    }
}

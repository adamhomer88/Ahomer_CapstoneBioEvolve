using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Water_Absorbtion
{
    [Serializable]
    class WaterAbsorbtionMutationProcessor : IWaterAbsorbtionMutationProcessor
    {
        private static WaterAbsorbtionMutationProcessor processor;

        public static WaterAbsorbtionMutationProcessor GetInstance()
        {
            if (processor == null)
                processor = new WaterAbsorbtionMutationProcessor();
            return processor;
        }
    }
}

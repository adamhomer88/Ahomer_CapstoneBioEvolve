using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Energy_Factory;
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


        public Plant Mutate(Plant plant)
        {
            MutateNewPhenotype(plant);
            return plant;
        }

        private void MutateExistingPhenotype(Plant plant)
        {
            plant.EnergyFactory.Mutate();
        }

        private void MutateNewPhenotype(Plant plant)
        {
            if (plant.EnergyFactory is WaterPermeableMembrane)
                plant.EnergyFactory = new Leaves();
            else
                MutateExistingPhenotype(plant);
        }
    }
}

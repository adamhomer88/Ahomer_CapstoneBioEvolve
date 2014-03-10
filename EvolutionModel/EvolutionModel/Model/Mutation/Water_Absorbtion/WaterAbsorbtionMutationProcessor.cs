using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Energy_Factory;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
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

        public Plant Mutate(Plant plant)
        {
            MutateNewPhenotype(plant);
            return plant;
        }

        private void MutateNewPhenotype(Plant plant)
        {
            INutrientAbsorber absorber = plant.NutrientAbsorbtion;
            if (absorber is WaterPermeableMembrane)
                plant.NutrientAbsorbtion = new Roots();
            else
            {
                MutateExistingAbsorbtionPhenotype(plant);
            }
        }

        private void MutateExistingAbsorbtionPhenotype(Plant plant)
        {
            plant.NutrientAbsorbtion.Mutate();
        }
    }
}

using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Water_Absorbtion
{
    [Serializable]
    class Roots : INutrientAbsorber
    {
        public int WaterAbsorbMultiplier { get; set; }
        public int NutrientAbsorbMultiplier { get; set; }

        private const int DEFAULT_WATER_ABSORB_MUTLIPLIER = 8;
        private const int DEFAULT_NUTRIENT_ABSORB_MUTLIPLIER = 8;

        public Roots()
        {
            this.WaterAbsorbMultiplier = DEFAULT_WATER_ABSORB_MUTLIPLIER;
            this.NutrientAbsorbMultiplier = DEFAULT_NUTRIENT_ABSORB_MUTLIPLIER;
        }

        public int absorbNutrients(EnvironmentTile localEnvironment, int mass)
        {
            int nutrientsAbsorbed = localEnvironment.removeNutrients(mass * NutrientAbsorbMultiplier);
            return nutrientsAbsorbed;
        }

        public int absorbWater(EnvironmentTile localEnvironment, int mass)
        {
            int waterAbsorbed = localEnvironment.RemoveWater(mass * WaterAbsorbMultiplier);
            return waterAbsorbed;
        }

        public IPhenotype Mutate()
        {
            int number = OrganismFactory.random.Next(2);
            if (number == 1)
                WaterAbsorbMultiplier++;
            else
                NutrientAbsorbMultiplier++;
            return this;
        }

        public override string ToString()
        {
            return "Roots";
        }
    }
}

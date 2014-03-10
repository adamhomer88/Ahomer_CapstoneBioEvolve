using EvolutionModel.Model.Environment;
using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Water_Absorbtion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    [Serializable]
    public class WaterPermeableMembrane : EnergyFactory, INutrientAbsorber
    {
        public int NutrientAbsorbMultiplier { get; set; }
        public int WaterAbsorbMultiplier { get; set; }

        #region Default Multipliers
        private const int DEFAULT_NUTRIENT_ABSORB_MULTIPLIER = 2;
        private const int DEFAULT_ENERGY_CREATION_MULTIPLIER = 2;
        private const int DEFAULT_WATER_ABSORB_MULTIPLIER = 2;
        #endregion

        public WaterPermeableMembrane()
        {
            this.NutrientAbsorbMultiplier = DEFAULT_NUTRIENT_ABSORB_MULTIPLIER;
            this.EnergyCreationMultiplier = DEFAULT_ENERGY_CREATION_MULTIPLIER;
            this.WaterAbsorbMultiplier = DEFAULT_WATER_ABSORB_MULTIPLIER;
        }

        public int absorbNutrients(EnvironmentTile localEnvironment, int mass)
        {
            int nutrientsAbsorbed = localEnvironment.removeNutrients(mass * NutrientAbsorbMultiplier);
            return nutrientsAbsorbed;
        }

        public int absorbWater(EnvironmentTile localEnvironment, int mass)
        {
            int waterAbsorbed = localEnvironment.RemoveWater(mass*WaterAbsorbMultiplier);
            return waterAbsorbed;
        }

        public override string ToString()
        {
            return "Water Permeable Membrane";
        }

        public override IPhenotype Mutate()
        {
            int number = OrganismFactory.random.Next(3);
            MutateAttribute(number);
            return this;
        }

        private void MutateAttribute(int number)
        {
            if (number == 0)
            {
                this.WaterAbsorbMultiplier++;
            }
            else if (number == 1)
            {
                this.EnergyCreationMultiplier++;
            }
            else
            {
                this.NutrientAbsorbMultiplier++;
            }
        }
    }
}

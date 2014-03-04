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
        const int NUTRIENT_ABSORB_MULTIPLIER = 5;
        const int ENERGY_CREATION_MULTIPLIER = 10;
        const int WATER_ABSORB_MULTIPLIER = 5;
        
        public int absorbNutrients(EnvironmentTile localEnvironment, int mass)
        {
            int nutrientsAbsorbed = localEnvironment.removeNutrients(mass * NUTRIENT_ABSORB_MULTIPLIER);
            return nutrientsAbsorbed;
        }

        public int absorbWater(EnvironmentTile localEnvironment, int mass)
        {
            int waterAbsorbed = localEnvironment.RemoveWater(mass*WATER_ABSORB_MULTIPLIER);
            return waterAbsorbed;
        }

        public override IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }

        public override void createEnergy(Plant plant)
        {
            int energyNeeded = DetermineEnergyNeeded(plant);
            if (plant.WaterTotal < energyNeeded || plant.NutrientTotal < energyNeeded)
            {
                if (plant.WaterTotal < plant.NutrientTotal)
                    energyNeeded = plant.WaterTotal;
                else
                    energyNeeded = plant.NutrientTotal;
            }
            resolveEnergyCreation(plant, energyNeeded);
        }

        private int DetermineEnergyNeeded(Plant plant)
        {
            int energyNeeded = ENERGY_CREATION_MULTIPLIER * plant.Mass;
            if (energyNeeded + plant.EnergyTotal > plant.MaxEnergy)
            {
                energyNeeded = (int)(plant.MaxEnergy - plant.EnergyTotal);
            }
            return energyNeeded;
        }

        private static void resolveEnergyCreation(Plant plant, int energyNeeded)
        {
            plant.NutrientTotal -= energyNeeded;
            plant.WaterTotal -= energyNeeded;
            plant.EnergyTotal += energyNeeded;
        }
    }
}

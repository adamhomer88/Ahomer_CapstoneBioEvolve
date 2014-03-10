using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    [Serializable]
    public abstract class EnergyFactory : IEnergyFactory
    {
        public int EnergyCreationMultiplier { get; set; }
        public abstract IPhenotype Mutate();

        protected int DetermineEnergyNeeded(Plant plant)
        {
            int energyNeeded = EnergyCreationMultiplier * plant.Mass;
            if (energyNeeded + plant.EnergyTotal > plant.MaxEnergy)
            {
                energyNeeded = (int)(plant.MaxEnergy - plant.EnergyTotal);
            }
            return energyNeeded;
        }

        public void createEnergy(Plant plant)
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

        protected virtual void resolveEnergyCreation(Plant plant, int energyNeeded)
        {
            plant.NutrientTotal -= energyNeeded;
            plant.WaterTotal -= energyNeeded;
            plant.EnergyTotal += energyNeeded;
        }
    }
}

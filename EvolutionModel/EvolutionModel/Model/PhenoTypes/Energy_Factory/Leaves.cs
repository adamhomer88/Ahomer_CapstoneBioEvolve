using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Energy_Factory
{
    [Serializable]
    public class Leaves : EnergyFactory
    {
        private double INCREASED_WATER_USAGE = 1.5;
        private double DECREASED_NUTRIENT_USAGE = .75;

        #region Mutation Constants
        private const double MUTATION_CHANGE = 0.05;
        private const double MIN_NUTRIENT_USAGE = .5;
        private const double MIN_MAX_WATER_USAGE = 1.25;
        #endregion

        protected override void resolveEnergyCreation(Plant plant, int energyNeeded)
        {
            plant.NutrientTotal -= (int)(energyNeeded*DECREASED_NUTRIENT_USAGE);
            plant.WaterTotal -= (int)(energyNeeded*INCREASED_WATER_USAGE);
            plant.EnergyTotal += energyNeeded;
        }

        public override IPhenotype Mutate()
        {
            int number = OrganismFactory.random.Next(2);
            if(number == 1)
            {
                if (INCREASED_WATER_USAGE != MIN_MAX_WATER_USAGE)
                    INCREASED_WATER_USAGE -= MUTATION_CHANGE;
            }
            else
            {
                if (DECREASED_NUTRIENT_USAGE != MIN_NUTRIENT_USAGE)
                    DECREASED_NUTRIENT_USAGE -= MUTATION_CHANGE;
            }
            return this;
        }

        public override string ToString()
        {
            return "Leaves";
        }
    }
}

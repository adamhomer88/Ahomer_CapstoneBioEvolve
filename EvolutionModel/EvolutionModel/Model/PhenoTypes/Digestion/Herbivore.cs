using EvolutionModel.Model.Genotypes;
using EvolutionModel.Model.PhenoTypes.Digestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    [Serializable]
    class Herbivore : DigestiveSystem
    {
        public Herbivore()
        {
            this.OrganismHungryFor = typeof(Plant);
        }

        public override int Digest(Genotypes.Plant plant)
        {
            int energyGained = (int)(plant.EnergyTotal * DigestiveSystem.ENERGY_MULTIPLIER * DigestiveSystem.FULLY_DIGESTIBLE);
            return energyGained;
        }

        public override int Digest(Genotypes.Animal animal)
        {
            int energyGained = (int)(animal.EnergyTotal * DigestiveSystem.ENERGY_MULTIPLIER * DigestiveSystem.NOT_DIGESTIBLE);
            return energyGained;
        }

        public override string ToString()
        {
            return "Herbivore";
        }
    }
}

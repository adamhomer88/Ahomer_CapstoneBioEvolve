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
    public class Carnivore : DigestiveSystem
    {
        public Carnivore()
        {
            this.OrganismHungryFor = typeof(Animal);
        }

        public override int Digest(Genotypes.Plant plant)
        {
            int energyGained = (int)(plant.Mass * DigestiveSystem.NOT_DIGESTIBLE * DigestiveSystem.ENERGY_MULTIPLIER);
            return energyGained;
        }

        public override int Digest(Genotypes.Animal animal)
        {
            int energyGained = (int)(animal.Mass * DigestiveSystem.FULLY_DIGESTIBLE * DigestiveSystem.ENERGY_MULTIPLIER);
            return energyGained;
        }

        public override string ToString()
        {
            return "Carnivore";
        }
    }
}

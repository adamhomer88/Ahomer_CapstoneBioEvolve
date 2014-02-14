using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    [Serializable]
    class ParasiticDigestiveSystem : DigestiveSystem
    {
        public double FavoredOrganismDigestRatio { get; set; }

        public override int Digest(Plant plant)
        {
            return absorbEnergyFromOrganism(plant, this.OrganismHungryFor);
        }

        public override int Digest(Animal animal)
        {
            return absorbEnergyFromOrganism(animal, this.OrganismHungryFor);
        }

        private int absorbEnergyFromOrganism(Organism organism, Type HungryFor)
        {
            int energy = organism.EnergyTotal;
            int absorbedEnergy = 0;
            if (this.OrganismHungryFor == HungryFor)
                absorbedEnergy = (int)(energy * FavoredOrganismDigestRatio);
            organism.EnergyTotal = organism.EnergyTotal - absorbedEnergy;
            return absorbedEnergy;
        }
    }
}

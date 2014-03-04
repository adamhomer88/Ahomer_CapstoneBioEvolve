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
            return (int)(plant.EnergyTotal * 0.5);
        }

        public override int Digest(Genotypes.Animal animal)
        {
            return (int)(animal.EnergyTotal * 0.05);
        }

        public override string ToString()
        {
            return "Herbivore";
        }
    }
}

using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    [Serializable]
    class Omnivore : DigestiveSystem
    {
        public Omnivore()
        {

        }

        public override int Digest(Plant plant)
        {
            return (int)(plant.EnergyTotal * .35);
        }

        public override int Digest(Animal animal)
        {
            return (int)(animal.EnergyTotal * .35);
        }
    }
}

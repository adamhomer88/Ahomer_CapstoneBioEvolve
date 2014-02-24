using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    [Serializable]
    public abstract class DigestiveSystem : IPhenotype
    {
        public const double FULLY_DIGESTIBLE = 0.5;
        public const double SEMI_DIGESTIBLE = 0.35;
        public const double NOT_DIGESTIBLE = 0.05;
        
        public Type OrganismHungryFor { get; set; }

        public abstract int Digest(Plant plant);
        public abstract int Digest(Animal animal);
        
        public int Digest(Organism organism)
        {
            int energy;
            if (organism is Plant)
                energy = Digest(organism as Plant);
            else
                energy = Digest(organism as Animal);
            return energy;
        }

        public IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}

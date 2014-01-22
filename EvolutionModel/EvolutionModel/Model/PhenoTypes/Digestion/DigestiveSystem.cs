using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Digestion
{
    public abstract class DigestiveSystem
    {
        public const double FULLY_DIGESTIBLE = 0.5;
        public const double SEMI_DIGESTIBLE = 0.35;
        public const double NOT_DIGESTIBLE = 0.05;

        public int MyProperty { get; set; }
        public Type AnimalHungryFor { get; set; }

        public abstract int Digest(Plant plant);
        public abstract int Digest(Animal animal);
        public abstract int Digest(Organism organism);

    }
}

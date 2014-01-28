using EvolutionModel.Model.PhenoTypes.Digestion;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Genotypes
{
    class OrganismFactory
    {
        public const Random random = new Random();
        public DigestiveFactory digestion = new DigestiveFactory();
        public LimbFactory limbs = new LimbFactory();
        private const int totalOrganismOptions = 3;

        public Organism getInstance()
        {
            Organism organism = randomOrganism();
            return organism;
        }

        private Organism randomOrganism()
        {
            int randomNumber = random.Next(totalOrganismOptions);
            Organism organism = null;
            switch (randomNumber)
            {
                case 0: organism = new Animal();
                    organism = randomizeAnimalBasePhenotypes(organism);
                    break;
                case 1: organism = new Plant();
                    organism = randomizePlantBasePhenotypes(organism);
                    break;
                case 2: organism = new Parasite();
                    break;
            }
            return organism;
        }

        private Organism randomizePlantBasePhenotypes(Organism organism)
        {
            throw new NotImplementedException();
        }

        private Organism randomizeAnimalBasePhenotypes(Organism organism)
        {
            organism.digestion = DigestiveFactory.getAnimalDigestiveSystem();
            return organism;
        }
    }
}

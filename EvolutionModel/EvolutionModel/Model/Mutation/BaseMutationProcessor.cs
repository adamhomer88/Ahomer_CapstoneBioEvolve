using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    class BaseMutationProcessor : IMutationProcessor
    {
        private const int DEFAULT_BASE_MUTATION_MARGIN = 5;

        public Organism MutateAnimal(Organism organism)
        {
            Animal mutatee = (Animal)mutateBaseOrganismPhenotypes(organism);
            return mutatee;
        }

        public Organism MutatePlant(Organism organism)
        {
            Plant mutatee = (Plant)mutateBaseOrganismPhenotypes(organism);
            return mutatee;
        }

        public Organism MutateParasite(Organism organism)
        {
            Parasite mutatee = (Parasite)mutateBaseOrganismPhenotypes(organism);
            return mutatee;
        }

        private Organism mutateBaseOrganismPhenotypes(Organism organism)
        {
            organism.MaximumMass += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN+1), DEFAULT_BASE_MUTATION_MARGIN);
            organism.MaxEnergy += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN + 1), DEFAULT_BASE_MUTATION_MARGIN);
            organism.EnergyPerTurn += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN + 1), DEFAULT_BASE_MUTATION_MARGIN);
            return organism;
        }
    }
}

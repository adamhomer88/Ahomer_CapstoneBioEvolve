using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    public class BaseMutationProcessor : IMutationProcessor
    {
        private const int DEFAULT_BASE_MUTATION_MARGIN = 5;
        private BaseMutationProcessor processor;

        public Organism MutateAnimal(Organism organism)
        {
            Animal mutatee = (Animal)mutateBaseOrganismPhenotypes(organism);
            mutatee = mutateBaseAnimalPhenotypes(mutatee);
            return mutatee;
        }

        public Organism MutatePlant(Organism organism)
        {
            Plant mutatee = (Plant)mutateBaseOrganismPhenotypes(organism);
            mutatee = mutateBasePlantPhenotypes(mutatee);
            return mutatee;
        }

        public Organism MutateParasite(Organism organism)
        {
            Parasite mutatee = (Parasite)mutateBaseOrganismPhenotypes(organism);
            mutatee = mutateBaseParasitePhenotypes(mutatee);
            return mutatee;
        }

        private Organism mutateBaseOrganismPhenotypes(Organism organism)
        {
            organism.MaximumMass += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN+1), DEFAULT_BASE_MUTATION_MARGIN);
            organism.MaxEnergy += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN + 1), DEFAULT_BASE_MUTATION_MARGIN);
            organism.EnergyPerTurn += OrganismFactory.random.Next(-(DEFAULT_BASE_MUTATION_MARGIN + 1), DEFAULT_BASE_MUTATION_MARGIN);
            return organism;
        }

        private Plant mutateBasePlantPhenotypes(Plant mutatee)
        {
            mutatee.growthThresholdToNutrients += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.growthRate += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.MaxNutrient += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.MaxWater += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.ReproductionRate += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            return mutatee;
        }

        private Parasite mutateBaseParasitePhenotypes(Parasite mutatee)
        {
            return mutatee;
        }

        private Animal mutateBaseAnimalPhenotypes(Animal mutatee)
        {
            mutatee.favoredHungerThreshold += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN+1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.unfavoredHungerThreshold += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            mutatee.reproductionThreshold += OrganismFactory.random.Next(-DEFAULT_BASE_MUTATION_MARGIN + 1, DEFAULT_BASE_MUTATION_MARGIN);
            return mutatee;
        }

        public BaseMutationProcessor getInstance()
        {
            if (this.processor == null)
                this.processor = new BaseMutationProcessor();
            return this.processor;
        }
    }
}

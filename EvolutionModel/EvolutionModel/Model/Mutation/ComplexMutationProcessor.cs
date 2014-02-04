using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    class ComplexMutationProcessor : IMutationProcessor
    {
        ComplexMutationProcessor processor;
        Random generator;

        public ComplexMutationProcessor()
        {
            generator = OrganismFactory.random;
        }

        public Genotypes.Organism MutateAnimal(Genotypes.Organism organism)
        {
            Animal mutatee = (Animal)(organism);
            int randomNum = generator.Next();
            throw new NotImplementedException();
        }

        public Genotypes.Organism MutatePlant(Genotypes.Organism organism)
        {
            Plant mutatee = (Plant)(organism);
            throw new NotImplementedException();
        }

        public Genotypes.Organism MutateParasite(Genotypes.Organism organism)
        {
            Parasite mutatee = (Parasite)(organism);
            throw new NotImplementedException();
        }


        public IMutationProcessor getInstance()
        {
            if(processor == null)
                processor = new ComplexMutationProcessor();
            return processor;
        }
    }
}

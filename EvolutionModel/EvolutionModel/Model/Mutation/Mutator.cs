using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    [Serializable]
    public class Mutator : IMutator
    {
        public IMutationProcessor processor {get; set;}

        private Dictionary<Type, Func<Organism, Organism>> mutationOptions;

        private Mutator()
        {
            processor = BaseMutationProcessor.GetInstance();
            mutationOptions = this.CreateDictionary();
        }

        public Genotypes.Organism Mutate(Genotypes.Organism organism)
        {
            Organism newMutatedOrganism = null;
            newMutatedOrganism = mutationOptions[organism.GetType()].Invoke(organism);
            return newMutatedOrganism;
        }

        public static Mutator GetBasicInstance()
        {
            Mutator mutator = new Mutator();
            mutator.processor = BaseMutationProcessor.GetInstance();
            return mutator;
        }

        public static Mutator GetComplexInstance()
        {
            Mutator mutator = new Mutator();
            mutator.processor = ComplexMutationProcessor.GetInstance();
            return mutator;
        }

        public Dictionary<Type, Func<Genotypes.Organism, Genotypes.Organism>> CreateDictionary()
        {
            return new Dictionary<Type, Func<Organism, Organism>>()
            {
                {typeof(Animal), processor.MutateAnimal},
                {typeof(Plant), processor.MutatePlant},
                {typeof(Parasite), processor.MutateParasite}
            };
        }
    }
}

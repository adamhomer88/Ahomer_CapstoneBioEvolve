using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    public abstract class Mutator : IMutator
    {
        public IMutationProcessor processor { get; set;}
        private Dictionary<Type, Func<Organism, Organism>> mutationOptions;

        public Mutator()
        {
            mutationOptions = this.CreateDictionary();
        }

        public Genotypes.Organism Mutate(Genotypes.Organism organism)
        {
            Organism newMutatedOrganism = null;
            mutationOptions[organism.GetType()].Invoke(organism);
            return newMutatedOrganism;
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

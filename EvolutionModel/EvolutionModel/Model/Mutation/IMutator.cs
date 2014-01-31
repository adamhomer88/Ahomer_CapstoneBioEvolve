using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    interface IMutator
    {
        Organism Mutate(Organism organism);
        Dictionary<Type, Func<Organism, Organism>> CreateDictionary();
    }
}

using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation
{
    public interface IMutationProcessor
    {
        Organism MutateAnimal(Organism organism);
        Organism MutatePlant(Organism organism);
        Organism MutateParasite(Organism organism);
    }
}

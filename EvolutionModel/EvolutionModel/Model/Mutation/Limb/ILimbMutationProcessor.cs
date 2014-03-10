using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Limb
{
    interface ILimbMutationProcessor
    {
        Animal MutateExistingLimb(Animal animal);
        Animal MutateNewLimb(Animal animal);
        Animal Mutate(Animal animal);
    }
}

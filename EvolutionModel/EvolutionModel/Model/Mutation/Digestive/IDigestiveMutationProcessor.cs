using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Digestive
{
    public interface IDigestiveMutationProcessor
    {
        Animal Mutate(Animal mutatee);
    }
}

using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Head
{
    interface IHeadMutationProcessor
    {
        Animal NewHead(Animal organism);
        Animal ModifyHead(Animal organism);
    }
}

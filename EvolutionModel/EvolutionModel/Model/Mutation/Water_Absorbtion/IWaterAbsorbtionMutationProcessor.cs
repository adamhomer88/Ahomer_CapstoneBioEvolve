using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.Mutation.Water_Absorbtion
{
    interface IWaterAbsorbtionMutationProcessor
    {
        Plant Mutate(Plant plant);
    }
}

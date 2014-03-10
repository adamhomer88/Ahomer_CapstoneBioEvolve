using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.AnimalStates
{
    [Serializable]
    class StarvingState : IAnimalState
    {
        public Organism ExecuteBehavior()
        {
            throw new NotImplementedException();
        }
    }
}

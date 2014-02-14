using EvolutionModel.Model.PhenoTypes.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Head
{
    [Serializable]
    class Horns : IProtectivePhenotype
    {
        public int doDefense()
        {
            throw new NotImplementedException();
        }

        public IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}

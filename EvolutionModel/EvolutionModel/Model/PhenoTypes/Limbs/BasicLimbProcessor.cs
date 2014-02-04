using EvolutionModel.Model.PhenoTypes.Head;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Limbs
{
    class BasicLimbProcessor : ILimbProcessor
    {
        public IAppendage createPredatoryLimb()
        {
            return new PredatoryLimb();
        }

        public IAppendage createUtilityLimb()
        {
            return new UtilityLimb();
        }
    }
}

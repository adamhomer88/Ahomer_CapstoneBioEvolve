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
        public Limb createPredatoryLimb()
        {
            return new PredatoryLimb();
        }

        public Limb createUtilityLimb()
        {
            return new UtilityLimb();
        }
    }
}

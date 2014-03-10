using EvolutionModel.Model.PhenoTypes.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Limbs
{
    [Serializable]
    class PredatoryLimb : Limb
    {
        public override string ToString()
        {
            return "Predatory Limb";
        }
    }
}

using EvolutionModel.Model.PhenoTypes.Skin;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Head
{
    [Serializable]
    class UtilityLimb : Limb
    {
        public override string ToString()
        {
            return "Utility Limb";
        }
    }
}

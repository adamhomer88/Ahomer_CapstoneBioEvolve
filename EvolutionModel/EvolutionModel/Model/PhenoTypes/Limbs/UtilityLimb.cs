using EvolutionModel.Model.PhenoTypes.Defensive_Offensive_Phenotypes;
using EvolutionModel.Model.PhenoTypes.Limbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Head
{
    class UtilityLimb : ILimb
    {
        public int moveBonus { get; set; }

        public int doAction()
        {
            return -1;
        }

        public int doMove()
        {
            return -1;
        }
    }
}

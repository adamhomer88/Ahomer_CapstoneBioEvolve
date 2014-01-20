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
        IProtectivePhenotype Phenotype;

        public int doAction()
        {
            return Phenotype.doDefense();
        }


        public int doMove()
        {
            return Phenotype.doMove();
        }
    }
}

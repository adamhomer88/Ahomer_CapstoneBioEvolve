using EvolutionModel.Model.PhenoTypes.Defensive_Offensive_Phenotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Limbs
{
    class PredatoryLimb : ILimb
    {
        IHarmfulPhenotype Phenotype { get; set; }

        public int doAction()
        {
            throw new NotImplementedException();
        }

        public int doMove()
        {
            throw new NotImplementedException();
        }
    }
}

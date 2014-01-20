using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Defensive_Offensive_Phenotypes
{
    interface IHarmfulPhenotype
    {
        int doAttack();
        int doMove();
    }
}

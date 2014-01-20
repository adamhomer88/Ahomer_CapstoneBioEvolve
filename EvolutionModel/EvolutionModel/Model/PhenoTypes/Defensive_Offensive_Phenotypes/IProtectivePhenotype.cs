using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Defensive_Offensive_Phenotypes
{
    public interface IProtectivePhenotype
    {
        int doDefense();
        int doMove();
    }
    
}

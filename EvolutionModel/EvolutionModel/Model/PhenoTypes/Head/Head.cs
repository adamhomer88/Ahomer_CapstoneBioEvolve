using EvolutionModel.Model.PhenoTypes.Defensive_Offensive_Phenotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Head
{
    class Head
    {
        public IProtectivePhenotype Protection { get; set; }
        public IHarmfulPhenotype Harmful { get; set; }
    }
}

using EvolutionModel.Model.PhenoTypes.Sensory_Phenotypes;
using EvolutionModel.Model.PhenoTypes.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Head
{
    public class Head : IPhenotype
    {
        public IProtectivePhenotype Protection { get; set; }

        public IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}

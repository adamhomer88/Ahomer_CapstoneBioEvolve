using EvolutionModel.Model.PhenoTypes.Skin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Head
{
    public class Head
    {
        public IProtectivePhenotype Protection { get; set; }
        public IHarmfulPhenotype Harmful { get; set; }
    }
}

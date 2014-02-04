using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.Model.PhenoTypes.Limbs
{
    public class Limb : IAppendage
    {
        public int DegradeFactor { get; set; }
        public int BaseSpeed { get; set; }

        public int doAction()
        {
            throw new NotImplementedException();
        }

        public IPhenotype Mutate()
        {
            throw new NotImplementedException();
        }
    }
}

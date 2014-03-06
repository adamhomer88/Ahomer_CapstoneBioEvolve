using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvolutionModel.UserControls.Creature
{
    [Serializable]
    public class Control_DeadAnimal : UserControl_Organism
    {
        public Control_DeadAnimal(DeadAnimal model) : base(model)
        {

        }
    }
}

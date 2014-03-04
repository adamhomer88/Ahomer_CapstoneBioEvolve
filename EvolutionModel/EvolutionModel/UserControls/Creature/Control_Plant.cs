using EvolutionModel.Model.Genotypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace EvolutionModel.UserControls.Creature
{
    [Serializable]
    class Control_Plant : UserControl_Organism
    {
        public Control_Plant(Plant Model) : base(Model)
        {
            this.VisualRepresentation.Fill = new SolidColorBrush(Colors.Ivory);
        }
    }
}

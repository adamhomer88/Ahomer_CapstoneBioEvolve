using EvolutionModel.Model.Genotypes;
using EvolutionModel.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace EvolutionModel.UserControls.Creature
{
    [Serializable]
    class Control_Animal : UserControl_Organism, Observer
    {
        public Control_Animal(Animal Model) : base(Model)
        {
            this.Model.setObserver(this);
            this.VisualRepresentation.Fill = new SolidColorBrush(Colors.Gold);
        }

        public void notify()
        {
            Dispatcher.BeginInvoke((Action)(()=>
            {
                Canvas.SetLeft(this, Model.Location.X);
                Canvas.SetTop(this, Model.Location.Y);
            }));
        }

        #region UnusedMethods
        public void notify(Animal a)
        {
            throw new NotImplementedException();
        }

        public void notify(Model.Environment.EnvironmentTile tile, Plant plant)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

using EvolutionModel.Model.Genotypes;
using EvolutionModel.ObserverPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvolutionModel.UserControls.Creature
{
    /// <summary>
    /// Interaction logic for Animal.xaml
    /// </summary>
    public partial class UserControl_Animal : UserControl, Observer
    {
        #region Delegates
        public delegate void SelectionAction(UserControl_Animal animal);
        #endregion

        public Animal Model { get; set; }
        public SelectionAction Selection { get; set; }

        public UserControl_Animal(Animal Model)
        {
            InitializeComponent();
            this.Model = Model;
            this.Model.setObserver(this);
        }

        private void Select_Animal(object sender, MouseButtonEventArgs e)
        {
            if (Selection != null)
                Selection.Invoke(this);
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

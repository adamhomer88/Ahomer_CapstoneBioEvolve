using EvolutionModel.Model.Genotypes;
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
    /// Interaction logic for Plant.xaml
    /// </summary>
    public partial class UserControl_Plant : UserControl
    {
        #region Delegates
        public delegate void SelectionAction(UserControl_Plant plant);
        #endregion

        public Plant Model { get; set; }
        public SelectionAction Selection { get; set; }

        public UserControl_Plant(Plant Model)
        {
            InitializeComponent();
            this.Model = Model;
        }

        private void Select_Plant(object sender, MouseButtonEventArgs e)
        {
            if (Selection != null)
                Selection.Invoke(this);
        }
    }
}

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
    /// Interaction logic for Animal.xaml
    /// </summary>
    public partial class UserControl_Animal : UserControl
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
        }

        private void Select_Animal(object sender, MouseButtonEventArgs e)
        {
            if (Selection != null)
                Selection.Invoke(this);
        }
    }
}

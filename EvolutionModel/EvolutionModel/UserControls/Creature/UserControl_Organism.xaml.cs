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
    /// Interaction logic for UserControl_Organism.xaml
    /// </summary>
    [Serializable]
    public partial class UserControl_Organism : UserControl
    {
        #region Delegates
        public delegate void SelectionAction(UserControl_Organism organism);
        #endregion
        public Ellipse VisualRepresentation { get; set; }
        public Organism Model { get; set; }
        public SelectionAction Selection { get; set; }

        public UserControl_Organism(Organism model)
        {
            InitializeComponent();
            this.VisualRepresentation = new Ellipse();
            VisualRepresentation.Height = 32;
            VisualRepresentation.Width = 32;
            this.VisualRepresentation.MouseRightButtonUp += Select_Organism;
            this.Canvas.Children.Add(VisualRepresentation);
            this.Model = model;

        }

        private void Select_Organism(object sender, MouseButtonEventArgs e)
        {
            if (Selection != null)
                Selection.Invoke(this);
        }
    }
}

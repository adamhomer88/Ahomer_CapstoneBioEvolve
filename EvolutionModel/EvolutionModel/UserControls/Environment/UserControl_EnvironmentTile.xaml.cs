using EvolutionModel.Model.Environment;
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

namespace EvolutionModel.UserControls.Environment
{
    /// <summary>
    /// Interaction logic for UserControl_EnvironmentTile.xaml
    /// </summary>
    public partial class UserControl_EnvironmentTile : UserControl
    {
        #region Delegates
        public delegate void SelectionAction(UserControl_EnvironmentTile tile);
        #endregion
        private bool _isSelected = false;
        public EnvironmentTile Model { get; set; }
        public SelectionAction Selection;

        public bool IsSelected 
        {
            get
            {
                return _isSelected;
            } 

            set
            {
                if (value)
                {
                    this.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
                    this.BorderThickness = new Thickness(1);
                }
                else
                {
                    this.BorderBrush = null;
                    this.BorderThickness = new Thickness(0);
                }
            } 
        }

        public UserControl_EnvironmentTile(EnvironmentTile tile, MainWindow ParentWindow)
        {
            InitializeComponent();
            Model = tile;
        }

        private void SelectTile_RightMouseButton(object sender, MouseButtonEventArgs e)
        {
            Selection.Invoke(this);
        }

    }
}
